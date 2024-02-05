using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class CategoryConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.Property(a => a.CategoryName)
                .IsRequired(true);
                            
            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvCategories)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
