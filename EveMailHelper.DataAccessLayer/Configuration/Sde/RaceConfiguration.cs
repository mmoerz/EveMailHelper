using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde.Character;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.ToTable(nameof(Race), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);
            builder.Property(a => a.EveId)
                .IsRequired()
                .ValueGeneratedNever();
                
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_NAME);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);
            builder.Property(a => a.ShortDescription)
                .HasMaxLength(Constants.SIZE_SHORTDESCRIPTION);

            builder.HasOne(a => a.Icon)
                .WithMany(p => p.ChrRaces)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
