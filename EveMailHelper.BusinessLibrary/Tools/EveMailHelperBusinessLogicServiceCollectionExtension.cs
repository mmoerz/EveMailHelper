using EveMailHelper.BusinessLibrary.Services;

using Microsoft.Extensions.DependencyInjection;

namespace at.gv.bmi.bk.Factotum.BusinessLogicLibrary.Tools
{
    public static class EveMailHelperBusinessLogicServiceCollectionExtension
    {

        /// <summary>
        /// provides the business logic services for factotum (NG)
        /// </summary>
        public static void AddEveMailHelperBusinessLogic(this IServiceCollection services)
        {
            //services.AddTransient<>

            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IEveMailService, EveMailService>();
            services.AddTransient<IEveMailTemplateService, EveMailTemplateService>();

        }

    }
}
