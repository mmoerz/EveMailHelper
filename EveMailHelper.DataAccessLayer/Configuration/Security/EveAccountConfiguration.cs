using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class EveAccountConfiguration : IEntityTypeConfiguration<EveAccount>
    {
        public void Configure(EntityTypeBuilder<EveAccount> builder)
        {
            builder.ToTable("EveAccount","Security");

            builder.Property(ea => ea.Name)
                .IsRequired(true)
                .HasMaxLength(200);
            builder.Property(ea => ea.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.HasMany(ea => ea.Characters)
                .WithOne(c => c.EveAccount)
                .HasForeignKey(c => c.EveAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
