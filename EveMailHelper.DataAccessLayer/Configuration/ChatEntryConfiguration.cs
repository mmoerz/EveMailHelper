using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class ChatEntryConfiguration : IEntityTypeConfiguration<ChatEntry>
    {
        public void Configure(EntityTypeBuilder<ChatEntry> builder)
        {
            builder.Property(ce => ce.Message)
                .IsRequired()
                .HasMaxLength(8000);
            builder.HasOne(ce => ce.Author)
                .WithMany()
                .HasForeignKey(ce => ce.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(ce => ce.Chat)
                .WithMany(ch => ch.Messages)
                .HasForeignKey(ce => ce.ChatId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
