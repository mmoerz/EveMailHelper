using EveMailHelper.DataModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailLabelConfigurationConfiguration : IEntityTypeConfiguration<EveMailLabel>
    {
        public void Configure(EntityTypeBuilder<EveMailLabel> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(ml => ml.Color)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(ml => ml.Character)
                .WithMany()
                .HasForeignKey(ml => ml.CharacterId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
