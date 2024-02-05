using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IndustryActivityProductConfiguration
        : IEntityTypeConfiguration<IndustryActivityProduct>
    {
        public void Configure(EntityTypeBuilder<IndustryActivityProduct> builder)
        {
            builder.ToTable(nameof(IndustryActivityProduct), Constants.SCHEMA_SDE);
            builder.HasKey(a => new { a.TypeId, a.ActivityId, a.ProductTypeId });

            builder.Property(a => a.Quantity)
                .IsRequired(true);
                            
            builder.HasOne(a => a.IndustryActivity)
                .WithMany()
                .HasForeignKey(a => new { a.TypeId, a.ActivityId })
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.ProductType)
                .WithMany()
                .HasForeignKey(a => a.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
