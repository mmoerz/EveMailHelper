using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.DataAccessLayer.Configuration.Market
{
    public partial class MarketPriceConfiguration : IEntityTypeConfiguration<MarketPrice>
    {
        public void Configure(EntityTypeBuilder<MarketPrice> builder)
        {
            // a table with an automated history
            builder.ToTable(nameof(MarketPrice), "market", e => e.IsTemporal());
            builder.HasKey(a => a.EveTypeId);
            builder.Property(a => a.EveTypeId)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasOne(a => a.EveType)
                .WithMany()
                .HasForeignKey(a => a.EveTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
