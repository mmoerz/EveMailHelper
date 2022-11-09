using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EveMailHelper.DataModels;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
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
