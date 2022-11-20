using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class MailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(16000);
            builder.Property(e => e.CreatedDate)
                .IsRequired();
            builder.HasOne(m => m.From)
                .WithMany()
                .HasForeignKey(m => m.FromId);
            builder.HasOne(e => e.EveMailTemplate)
                .WithMany(emt => emt.EveMailsGenerated)
                .HasForeignKey(e => e.EveMailTemplateId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
