using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EveMailHelper.DataAccessLayer.Context
{
    /// <summary>
    /// for runtime db context creation with the correct configuration settings
    /// is needed for command line tool migration/update
    /// for ASP.NET factory (and blazor alike) see projects readme.md for details
    /// </summary>
    public class EveMailHelperContextFactory : IDesignTimeDbContextFactory<EveMailHelperContext>
    {
        private IConfiguration Configuration => EveMailHelperContext.SetupConfiguration();

        public EveMailHelperContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EveMailHelperContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("Default"));

            return new EveMailHelperContext(builder.Options);
        }
    }
}
