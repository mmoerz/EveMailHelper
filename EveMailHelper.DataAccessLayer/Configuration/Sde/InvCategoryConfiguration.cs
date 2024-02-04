using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class InvCategoryConfiguration
        : IEntityTypeConfiguration<InvCategory>
    {
        public void Configure(EntityTypeBuilder<InvCategory> builder)
        {
            builder.ToTable(nameof(InvCategory), Constants.SCHEMA_SDE);
            builder.HasAlternateKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.Property(a => a.CategoryName)
                .IsRequired(true);
                            
            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvCategories)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
