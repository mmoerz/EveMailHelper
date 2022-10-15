using Microsoft.Extensions.DependencyInjection;

namespace at.gv.bmi.bk.Factotum.BusinessLogicLibrary.Tools
{
    public static class EveMailHelperBusinessLogicServiceCollectionExtension
    {

        /// <summary>
        /// provides the business logic services for factotum (NG)
        /// </summary>
        public static void AddFactotumBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IAccountsUserService, AccountsUserService>();
            

            
        }

    }
}
