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
            builder.HasKey(a => new { a.TypeId, a.ActivityId, a.ProductTypeId });

            builder.Property(a => a.Probability)
                .HasColumnType("decimal(3, 2)")
                .IsRequired(true);
                            
            builder.HasOne(a => a.IndustryActivity)
                .WithMany(i => i.Probabilities)
                .HasForeignKey(a => new { a.TypeId, a.ActivityId })
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.ProductType)
                .WithMany()
                .HasForeignKey(a => a.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
