using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role","Security");

            builder.Property(r => r.Name)
                .HasMaxLength(200);
            builder.Property(r => r.Description)
                .HasMaxLength(1000);

            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles);
        }
    }
}
