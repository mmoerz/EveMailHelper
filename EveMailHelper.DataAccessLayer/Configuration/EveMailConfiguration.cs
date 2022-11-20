using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(8000);
            builder.Property(e => e.CreatedDate)
                .IsRequired();
            builder.HasOne(e => e.EveMailTemplate)
                .WithMany(emt => emt.EveMailsGenerated)
                .HasForeignKey(e => e.EveMailTemplateId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
