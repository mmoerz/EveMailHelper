using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class InvTypeGroupConfiguration
        : IEntityTypeConfiguration<InvType>
    {
        public void Configure(EntityTypeBuilder<InvType> builder)
        {
            builder.ToTable(nameof(InvType), Constants.SCHEMA_SDE);
            builder.HasAlternateKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.Property(a => a.TypeName)
                .HasMaxLength(Constants.SIZE_NAME);

            builder.Property(a => a.Description)
                //.IsRequired()
                .HasMaxLength(Constants.SIZE_DESCRIPTION);

            builder.HasIndex(a => a.GroupId);

            builder.Property(a => a.BasePrice)
                .HasColumnType("decimal(19, 4)");
                

            builder.HasOne(d => d.Group)
                .WithMany(p => p.InvTypes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvTypes)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.MarketGroup)
                .WithMany(p => p.InvTypes)
                .HasForeignKey(d => d.MarketGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Race)
                .WithMany(p => p.InvTypes)
                .HasForeignKey(a => a.RaceId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
