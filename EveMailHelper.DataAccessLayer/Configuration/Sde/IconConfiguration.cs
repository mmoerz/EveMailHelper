using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IconConfiguration : IEntityTypeConfiguration<Icon>
    {
        public void Configure(EntityTypeBuilder<Icon> builder)
        {
            builder.ToTable(nameof(Icon), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);
            builder.Property(a => a.IconFile)
                .HasMaxLength(Constants.SIZE_FILENAME);
        }
    }
}
