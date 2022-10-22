using at.gv.bmi.bk.Factotum.BusinessLogicLibrary.Tools;

using EveMailHelper.DataAccessLayer.Context;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;

using NLog.Web;

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
        x => x.MigrationsAssembly("at.gv.bmi.bk.DAEMeldestelle.DAL")));
#endregion

#region NLog Registration
builder.Host.UseNLog();
#endregion

#region internal Services
builder.Services.AddEveMailHelperChatLogParser();
builder.Services.AddEveMailHelperBusinessLogic();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
var JsonIISSettings = builder.Configuration.GetSection("IISSettings");

var basePath = JsonIISSettings.GetValue<string>("FactotumImportBasePath", "/");
app.Logger.LogInformation($"FactotumImportBasePath loaded : '{basePath}'");

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
    app.Logger.LogInformation($"FactotumImportBasePath is: '{basePath}'");
    app.UsePathBase(basePath);
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production
    // scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();