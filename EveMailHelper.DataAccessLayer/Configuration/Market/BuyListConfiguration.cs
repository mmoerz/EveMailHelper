using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.DataAccessLayer.Configuration.Market
{
    public partial class BuyListConfiguration : IEntityTypeConfiguration<BuyList>
    {
        public void Configure(EntityTypeBuilder<BuyList> builder)
        {
            // a table with an automated history
            builder.ToTable(nameof(BuyList), "market");
        }
    }
}
