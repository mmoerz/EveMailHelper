using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using Microsoft.VisualBasic;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class CorporationConfiguration : IEntityTypeConfiguration<Corporation>
    {
        public void Configure(EntityTypeBuilder<Corporation> builder)
        {
            
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(a => a.DateFounded)
                .IsRequired(false);
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(8000);
            builder.Property(a => a.FactionId)
                .IsRequired(false);
            builder.Property(a => a.HomeStationId)
                .IsRequired(false);
            builder.Property(a => a.Shares)
                .IsRequired(false);
            builder.Property(a => a.Ticker)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(a => a.Url)
                .IsRequired(false)
                .HasMaxLength(200);
            
            // AllianceId is done in AllianceConfiguration
            builder.HasOne(a => a.Ceo)
                .WithOne()
                .HasForeignKey<Corporation>(a => a.CeoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(a => a.Creator)
                .WithOne()
                .HasForeignKey<Corporation>(a => a.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(a => a.Members)
                .WithOne(ch => ch.Corporation)
                .HasForeignKey(ch => ch.CorporationId)
                .OnDelete(DeleteBehavior.SetNull);
            //builder.HasOne(c => c.Alliance)
            //    .WithMany(a => a.Corporations)
            //    .HasForeignKey(c => c.AllianceId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
