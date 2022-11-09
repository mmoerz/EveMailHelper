using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Context
{
    public partial class EveMailHelperContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EveMailHelperContext).Assembly);

            //modelBuilder.Entity<Character>()
            //    .HasData(new Character()
            //    {
            //        Name="moerz01",
                    
            //    }
            //    );

        }
    }
}
