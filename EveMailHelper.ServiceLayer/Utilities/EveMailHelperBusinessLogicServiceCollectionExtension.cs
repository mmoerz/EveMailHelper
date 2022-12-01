using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.BusinessLibrary.Services;

using Microsoft.Extensions.DependencyInjection;
using EveMailHelper.ServiceLayer.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EveMailHelper.ServiceLibrary.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class EveMailHelperServiceLayerServiceCollectionExtension
    {
        /// <summary>
        /// provides the business logic services for eve helper logic
        /// </summary>
        public static void AddEveMailHelperServices(this IServiceCollection services)
        {
            // accounts, eve accounts, characters and permissions are handled here
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();

            // only basic account and eve account handling
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IMailManager, MailManager>();
            services.AddTransient<IEveMailTemplateService, EveMailTemplateService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IChatService, ChatService>();

            services.AddTransient<IInGameMailManager, InGameMailManager>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="loginPath">Path for authentication controller</param>
        public static void AddEveMailStandardServices(
            this IServiceCollection services,
            string loginPath)
        {
            services.Configure<CookiePolicyOptions>(options =>
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = loginPath;
                });
        }
    }
}
