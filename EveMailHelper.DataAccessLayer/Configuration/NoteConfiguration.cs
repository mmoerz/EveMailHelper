using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(4000);
            builder.HasOne(n => n.AttachedTo)
                .WithMany(ch => ch.Notes)
                .HasForeignKey(n => n.AttachedToId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
        }
    }
}
