using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", "Security");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            //builder.HasMany(a => a.Roles)
            //    .WithOne(c => c.EveAccount)
            //    .HasForeignKey(c => c.EveAccountId);
        }
    }
}
