using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class IndustryActivityConfiguration 
        : IEntityTypeConfiguration<IndustryActivity>
    {
        public void Configure(EntityTypeBuilder<IndustryActivity> builder)
        {
            builder.ToTable(nameof(IndustryActivity), Constants.SCHEMA_SDE);
            builder.HasKey(a => new { a.TypeId, a.ActivityId });
            builder.HasIndex(a => a.ActivityId);
            builder.Property(a => a.ActivityId)
                .IsRequired();
                
            builder.HasOne(a => a.Type)
                .WithMany(p => p.IndustryActivities)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
