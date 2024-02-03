using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IndustryActivityProbabilityConfiguration
        : IEntityTypeConfiguration<IndustryActivityProbability>
    {
        public void Configure(EntityTypeBuilder<IndustryActivityProbability> builder)
        {
            builder.ToTable(nameof(IndustryActivityProbability), Constants.SCHEMA_SDE);
            builder.HasAlternateKey(a => new { a.TypeId, a.ActivityId, a.ProductTypeId });

            builder.Property(a => a.Probability)
                .IsRequired(true);
                            
            builder.HasOne(a => a.IndustryActivity)
                .WithMany()
                .HasForeignKey(a => new { a.TypeId, a.ActivityId })
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.ProductType)
                .WithMany()
                .HasForeignKey(a => a.ProductTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
