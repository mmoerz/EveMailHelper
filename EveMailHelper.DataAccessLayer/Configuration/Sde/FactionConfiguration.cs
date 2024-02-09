using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class FactionConfiguration : IEntityTypeConfiguration<Faction>
    {
        public void Configure(EntityTypeBuilder<Faction> builder)
        {
            builder.ToTable(nameof(Faction), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_NAME);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_TEXT_MAX);

            builder.HasOne(f => f.Race)
                .WithMany(r => r.Factions)
                .HasForeignKey(r => r.RaceId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(f => f.SolarSystem)
                .WithMany()
                .HasForeignKey(f => f.SolarSystemId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(f => f.Corporation)
                .WithOne()
                .HasForeignKey<Faction>(f => f.CorporationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(f => f.MilitiaCorporation) 
                .WithOne()
                .HasForeignKey<Faction>(f => f.MilitiaCorporationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(f => f.Icon)
                .WithMany()
                .HasForeignKey(f => f.IconId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
