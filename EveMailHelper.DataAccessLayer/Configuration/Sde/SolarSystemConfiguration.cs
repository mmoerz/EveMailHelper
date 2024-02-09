using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class SolarSystemConfiguration : IEntityTypeConfiguration<SolarSystem>
    {
        public void Configure(EntityTypeBuilder<SolarSystem> builder)
        {
            builder.ToTable(nameof(SolarSystem), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_NAME);
            
            builder.HasOne(s => s.Region)
                .WithMany(r => r.SolarSystems)
                .HasForeignKey(s => s.RegionId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(s => s.Constellation)
                .WithMany(c => c.SolarSystems)
                .HasForeignKey(s => s.ConstellationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(r => r.Faction)
                .WithMany(f => f.SolarSystems)
                .HasForeignKey(r => r.FactionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
