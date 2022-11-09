using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.BusinessLibrary.Services;

using Microsoft.Extensions.DependencyInjection;
using EveMailHelper.ServiceLayer.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class EveMailHelperServiceLayerServiceCollectionExtension
    {
        /// <summary>
        /// provides the business logic services for eve helper logic
        /// </summary>
        public static void AddEveMailHelperServices(this IServiceCollection services)
        {
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IEveMailService, EveMailService>();
            services.AddTransient<IEveMailTemplateService, EveMailTemplateService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IChatService, ChatService>();
        }

        public static void AddEveMailStandardServices(this IServiceCollection services)
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


        }
    }
}
