using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;

using NLog.Web;

using EveMailHelper.BusinessLogicLibrary.Utilities;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.DataAccessLayer.Context;
using EVEStandard.Enumerations;
using EVEStandard;
using Microsoft.AspNetCore.Authentication.Cookies;
using NLog;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLibrary.Managers;

#region NLog Initialization for Programm.cs
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main reached");
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddMudServices();

    #region Database Access
    // factory for 'building' db access on the fly
    builder.Services.AddDbContextFactory<EveMailHelperContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default"),
            x => x.MigrationsAssembly("EveMailHelper.DataAccessLayer")));
    #endregion

    #region EVEStandard Initialization
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        // This is set to false to allow the SSOState cookie to be persisted in the user's session cookie as
        // required by the auth security check. If you need to set this value to true you should refer to 
        // https://docs.microsoft.com/en-us/aspnet/core/security/gdpr for additional guidance.
        //options.IdleTimeout = TimeSpan.FromHours(4);
        options.CheckConsentNeeded = context => false;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    // Add cookie authentication and set the login url
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
        });

    // Initialize the client
    var esiClient = new EVEStandardAPI(
            "EveMailHelpers",                  // User agent
            DataSource.Tranquility,         // Server [Tranquility/Singularity]
            TimeSpan.FromSeconds(30));       // Timeout

    // Register with DI container
    builder.Services.AddSingleton<EVEStandardAPI>(esiClient);

    // TODO: this is not the best way to check for config file parts
    builder.CheckIfMainConfigItemExists("SSOCallbackUrl");
    builder.CheckIfMainConfigItemExists("ClientId");
    builder.CheckIfMainConfigItemExists("SecretKey");
    // Register EVEStandard SSO if you want to use it, you don't have to use EVEStandard SSO with EVEStandard.
    var sso = new SSOv2(
        DataSource.Tranquility,
        builder.Configuration["SSOCallbackUrl"],
        builder.Configuration["ClientId"],
        builder.Configuration["SecretKey"]);
    builder.Services.AddSingleton<SSOv2>(sso);

    // Session is required 
    builder.Services.AddSession(options =>
    {
        // added to reduce / remove errors from middleware ? first try
        // should remove the nasty error messages
        options.IdleTimeout = TimeSpan.FromHours(4);
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    });

    // register Auth Controller (Auth/Login, Auth/Logout, Auth/Callback)
    builder.Services.AddControllers();
    //builder.Services.AddControllersWithViews();

    #endregion

    #region NLog Registration
    builder.Host.UseNLog();
    #endregion

    #region internal services of this tool
    builder.Services.AddEveMailHelperChatLogParser();
    builder.Services.AddEveMailHelperServices();
    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    var JsonIISSettings = builder.Configuration.GetSection("IISSettings");

    var basePath = JsonIISSettings.GetValue<string>("ApplicationBasePath", "/");
    app.Logger.LogInformation($"ApplicationBasePath loaded : '{basePath}'");

    // load Settings from config file
    var forceProduction = JsonIISSettings.GetValue<bool>("force", true);
    if (forceProduction)
        app.Logger.LogInformation("forced load of IIS Production Settings");
    else
        app.Logger.LogInformation("no IIS Production forced");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    else
    {
        //basePath = Environment.GetEnvironmentVariable("FACTOTUMIMPORT_PATHBASE");
        //app.Logger.LogInformation("Environment FACTOTUMIMPORT_PATHBASE:" +
        //    $"{basePath}");
        app.Logger.LogInformation($"forcing IIS Production environment");
        app.Logger.LogInformation($"ApplicationBasePath is: '{basePath}'");
        app.UsePathBase(basePath);
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production
        // scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<EveMailHelperContext>();
        context.Database.EnsureCreated();
        //TODO: DbInitializer.Initialize(context);
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    #region EveStandard SSO Authentication requirements
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization(); // EveStandard
    app.UseSession();
    app.MapControllers(); // for cross site authentification
    #endregion

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped - cause: exception");
    throw;
}
finally
{
    // ensure flushing of cached messages
    // and proper shutdown for NLog to avoid loosing anything
    NLog.LogManager.Shutdown();
}