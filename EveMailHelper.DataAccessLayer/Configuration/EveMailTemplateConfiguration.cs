using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailTemplateConfiguration : IEntityTypeConfiguration<EveMailTemplate>
    {
        public void Configure(EntityTypeBuilder<EveMailTemplate> builder)
        {
            builder.ToTable("EveMailTemplate");

            builder.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(8000);
            builder.Property(e => e.CreatedDate)
                .IsRequired();
        }
    }
}
