using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable(nameof(Region), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_NAME);

            builder.HasOne(r => r.Faction)
                .WithMany(f => f.Regions)
                .HasForeignKey(r => r.FactionId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
