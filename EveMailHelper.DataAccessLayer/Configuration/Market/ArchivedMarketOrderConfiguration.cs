using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.DataAccessLayer.Configuration.Market
{
    public partial class MarketOrderConfiguration : IEntityTypeConfiguration<MarketOrder>
    {
        public void Configure(EntityTypeBuilder<MarketOrder> builder)
        {
            // a table with an automated history
            builder.ToTable(nameof(MarketOrder), "market", e => e.IsTemporal());
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(n => n.Range)
                .HasMaxLength(Constants.SIZE_TEXT);

            builder.HasOne(a => a.SolarSystem)
                .WithMany()
                .HasForeignKey(a => a.SolarSystemId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.EveType)
                .WithMany()
                .HasForeignKey(a => a.EveId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
