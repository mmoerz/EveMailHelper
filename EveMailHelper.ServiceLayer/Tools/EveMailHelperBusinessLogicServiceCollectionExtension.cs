using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.BusinessLibrary.Services;

using Microsoft.Extensions.DependencyInjection;
using EveMailHelper.ServiceLayer.Managers;

namespace EveMailHelper.ServiceLayer.Tools
{
    public static class EveMailHelperBusinessLogicServiceCollectionExtension
    {
        /// <summary>
        /// provides the business logic services for eve helper logic
        /// </summary>
        public static void AddEveMailHelperBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IEveMailService, EveMailService>();
            services.AddTransient<IEveMailTemplateService, EveMailTemplateService>();
            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IChatService, ChatService>();
        }
    }
}
