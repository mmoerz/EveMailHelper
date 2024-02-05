using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IndustryActivityMaterialConfiguration
        : IEntityTypeConfiguration<IndustryActivityMaterial>
    {
        public void Configure(EntityTypeBuilder<IndustryActivityMaterial> builder)
        {
            builder.ToTable(nameof(IndustryActivityMaterial), Constants.SCHEMA_SDE);
            builder.HasKey(a => new { a.TypeId, a.ActivityId, a.MaterialTypeId });
            builder.HasIndex(a => a.ActivityId);
            builder.Property(a => a.ActivityId)
                .IsRequired();
                
            builder.HasOne(a => a.Type)
                .WithMany()
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.IndustryActivity)
                .WithMany()
                .HasForeignKey(a => new { a.TypeId, a.ActivityId })
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.MaterialType)
                .WithMany()
                .HasForeignKey(a => a.MaterialTypeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
