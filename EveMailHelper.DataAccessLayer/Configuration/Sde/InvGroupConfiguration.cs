using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class InvGroupConfiguration
        : IEntityTypeConfiguration<InvGroup>
    {
        public void Configure(EntityTypeBuilder<InvGroup> builder)
        {
            builder.ToTable(nameof(InvGroup), Constants.SCHEMA_SDE);
            builder.HasAlternateKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.HasIndex(e => e.CategoryId);

            builder.Property(a => a.GroupName)
                .HasMaxLength(Constants.SIZE_TEXT)
                .IsRequired(true);

            builder.HasOne(d => d.Category)
                .WithMany(p => p.InvGroups)
                .HasForeignKey(d => d.CategoryId);

            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvGroups)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
