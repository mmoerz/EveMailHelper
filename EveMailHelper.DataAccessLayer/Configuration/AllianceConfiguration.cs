using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class AllianceConfiguration : IEntityTypeConfiguration<Alliance>
    {
        public void Configure(EntityTypeBuilder<Alliance> builder)
        {
            
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(a => a.Ticker)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasOne(a => a.CreatorCorporation)
                .WithOne()
                .HasForeignKey<Alliance>(a => a.CreatorCorporationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Creator)               
                .WithOne()
                .HasForeignKey<Alliance>(a => a.CreatorId);
            builder.HasMany(a => a.Corporations)
                .WithOne(c => c.Alliance)
                .HasForeignKey(c => c.AllianceId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.ExecutorCorporation)
                .WithOne()
                .HasForeignKey<Alliance>(a => a.ExecutorCorporationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
