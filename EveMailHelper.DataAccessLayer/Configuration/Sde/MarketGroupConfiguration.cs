using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class MarketGroupConfiguration
        : IEntityTypeConfiguration<MarketGroup>
    {
        public void Configure(EntityTypeBuilder<MarketGroup> builder)
        {
            builder.ToTable(nameof(MarketGroup), Constants.SCHEMA_SDE);
            builder.HasAlternateKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);

            builder.Property(a => a.MarketGroupName)
                .HasMaxLength(Constants.SIZE_TEXT)
                .IsRequired(true);

            builder.HasOne(d => d.ParentGroup)
                .WithMany(p => p.InverseParentGroup)
                .HasForeignKey(d => d.ParentGroupId);

            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvMarketGroups)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
