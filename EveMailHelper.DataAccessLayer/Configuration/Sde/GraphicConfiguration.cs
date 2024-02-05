using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class GraphicConfiguration : IEntityTypeConfiguration<Graphic>
    {
        public void Configure(EntityTypeBuilder<Graphic> builder)
        {
            builder.ToTable(nameof(Graphic), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.SofFactionName)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_TEXT);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);
            builder.Property(a => a.SofHullName)
                .HasMaxLength(Constants.SIZE_TEXT);
            builder.Property(a => a.SofRaceName)
                .HasMaxLength (Constants.SIZE_TEXT);
            builder.Property(a => a.GraphicFile)
                .HasMaxLength(Constants.SIZE_FILENAME);
        }
    }
}
