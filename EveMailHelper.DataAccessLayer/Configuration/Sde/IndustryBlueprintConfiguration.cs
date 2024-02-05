using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IndustryBlueprintConfiguration
        : IEntityTypeConfiguration<IndustryBlueprint>
    {
        public void Configure(EntityTypeBuilder<IndustryBlueprint> builder)
        {
            builder.ToTable(nameof(IndustryBlueprint), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.TypeId);

            builder.Property(a => a.TypeId)
                .ValueGeneratedNever();

            builder.Property(a => a.MaxProductionLimit)
                .IsRequired(true);
                            
            builder.HasOne(a => a.Type)
                .WithOne(p => p.IndustryBlueprint)
                .HasForeignKey<IndustryBlueprint>(a => a.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
