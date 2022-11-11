using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Context
{
    public partial class EveMailHelperContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EveMailHelperContext).Assembly);

            modelBuilder.Entity<Account>()
                .HasData(new Account()
                {
                    Id = Guid.Empty,
                    NickName = "Default",
                    FirstName = "Default",
                    LastName = "Default",
                    Description = "Default Account"
                });

            modelBuilder.Entity<EveAccount>()
                .HasData(new EveAccount()
                {
                    Id = Guid.Empty,
                    Name = "Default",
                    Description = "Default Account"
                });
        }
    }
}
