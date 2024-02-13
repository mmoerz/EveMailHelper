using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.DataAccessLayer.Configuration.Market
{
    public partial class BuyListItemConfiguration : IEntityTypeConfiguration<BuyListItem>
    {
        public void Configure(EntityTypeBuilder<BuyListItem> builder)
        {
            // a table with an automated history
            builder.ToTable(nameof(BuyListItem), "market");

            builder.HasOne(i => i.BuyList)
                .WithMany(b => b.ItemList)
                .HasForeignKey(b => b.BuyListId)
                .OnDelete(DeleteBehavior.ClientCascade);

            // thx shitty data integrity
            builder.HasOne(a => a.EveType)
                .WithMany()
                .HasForeignKey(a => a.EveTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
