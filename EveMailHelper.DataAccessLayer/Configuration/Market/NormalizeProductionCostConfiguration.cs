using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.DataAccessLayer.Configuration.Market
{
    public partial class NormalizeProductionCostConfiguration : IEntityTypeConfiguration<NormalizeProductionCost>
    {
        public void Configure(EntityTypeBuilder<NormalizeProductionCost> builder)
        {
            // a table with an automated history
            builder.ToTable(nameof(NormalizeProductionCost), "market");
            builder.HasKey(a => new { a.EveTypeId, a.ActivityId });
            //builder.HasIndex(a => a.ActivityId);
            builder.Property(a => a.ActivityId)
                .IsRequired();

            builder.Property(a => a.DirectCostSum)
                .HasComputedColumnSql("[DirectJobCost] + [DirectComponentCost]");
            builder.Property(a => a.BestPriceSum)
                .HasComputedColumnSql("[BestPriceJobCost] + [BestPriceComponentCost]");
            builder.Property(a => a.ProductCostSum)
                .HasComputedColumnSql("[ProductQuantity] * [ProductPricePerUnit]");

            builder.HasOne(a => a.EveType)
                .WithMany()
                .HasForeignKey(a => a.EveTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.IndustryActivity)
                .WithMany(i => i.NormalizeProductionCosts)
                .HasForeignKey(a => new { a.EveTypeId, a.ActivityId })
                .OnDelete(DeleteBehavior.ClientCascade);

            // thx shitty data integrity
            builder.HasOne(a => a.Product)
                .WithMany()
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
