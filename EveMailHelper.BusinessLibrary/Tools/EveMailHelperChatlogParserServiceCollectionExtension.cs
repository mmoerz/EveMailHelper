using EveMailHelper.ChatLogParser;

using Microsoft.Extensions.DependencyInjection;

namespace EveMailHelper.BusinessLogicLibrary.Tools
{
    public static class EveMailHelperChatlogParserServiceCollectionExtension
    {
        /// <summary>
        /// provides the business logic services for eve helper logic
        /// </summary>
        public static void AddEveMailHelperChatLogParser(this IServiceCollection services)
        {
            services.AddTransient<IChatLogParser, ChatLogParser.ChatLogParser>();
        }
    }
}
