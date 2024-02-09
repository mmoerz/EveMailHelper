using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class NpcCorporationConfiguration : IEntityTypeConfiguration<NpcCorporation>
    {
        public void Configure(EntityTypeBuilder<NpcCorporation> builder)
        {
            builder.ToTable(nameof(NpcCorporation), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);

            builder.HasOne(c => c.Friend)
                .WithOne(oc => oc.Friend)
                .HasForeignKey<NpcCorporation>(c => c.FriendId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.Enemy)
                .WithOne(oc => oc.Enemy)
                .HasForeignKey<NpcCorporation>(c => c.EnemyId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.SolarSystem)
                .WithMany()
                .HasForeignKey(c => c.SolarSystemId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.Faction)
                .WithMany(f => f.NpcCorporations)
                .HasForeignKey(c => c.FactionId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.Icon)
                .WithMany()
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
