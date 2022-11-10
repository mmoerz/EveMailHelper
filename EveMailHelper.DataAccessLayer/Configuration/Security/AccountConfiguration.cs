using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account","Security");

            builder.Property(a => a.NickName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.FirstName)
                .HasMaxLength(200);
            builder.Property(a => a.LastName)
                .HasMaxLength(200);
            builder.Property(a => a.Email)
                .HasMaxLength(200);
            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.HasMany(a => a.Characters)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId);

            builder.HasMany(a => a.EveAccounts)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId);

            builder.HasMany(a => a.Roles)
                .WithMany(r => r.Accounts);
                
        }
    }
}
