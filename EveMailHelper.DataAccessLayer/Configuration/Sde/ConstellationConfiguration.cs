using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class ConstellationConfiguration : IEntityTypeConfiguration<Constellation>
    {
        public void Configure(EntityTypeBuilder<Constellation> builder)
        {
            builder.ToTable(nameof(Constellation), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_NAME);

            builder.HasOne(c => c.Region)
                .WithMany(r => r.Constellations)
                .HasForeignKey(r => r.RegionId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.Faction)
                .WithMany(f => f.Constellations)
                .HasForeignKey(c => c.FactionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
