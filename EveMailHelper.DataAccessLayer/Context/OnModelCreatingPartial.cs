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

            modelBuilder.Entity<Corporation>()
                .HasData(new Corporation()
                {
                    Id = Guid.Parse("11110000-0000-0000-0000-000011110000"),
                    Name = "Noname Default",
                    EveId = 0,
                    Description = "Noname Default",
                    DateFounded = DateTime.Now,
                    Ticker = "Noname Default"
                }
                );

            //modelBuilder.Entity<Character>()
            //    .HasData(new Character()
            //    {
            //        Id = Guid.Empty,
            //        Name = "Admin",
            //        Age = 0,
            //        Birthday = DateTime.Now,
            //        CreatedDate= DateTime.Now,
            //        Account= new Account() { }
            //    }
            //    ) ;

            //modelBuilder.Entity<Account>()
            //    .HasData(new Account()
            //    {
            //        Id = Guid.Empty,
            //        NickName = "Default",
            //        FirstName = "Default",
            //        LastName = "Default",
            //        Description = "Default Account"
            //    });

            //modelBuilder.Entity<EveAccount>()
            //    .HasData(new EveAccount()
            //    {
            //        Id = Guid.Empty,
            //        Name = "Default",
            //        Description = "Default Account"
            //    });

        }
    }
}
