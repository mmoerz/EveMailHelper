using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(c => c.ChannelName)
                .IsRequired()
                .HasMaxLength(150);
            builder.HasOne(c => c.AttachedTo)
                .WithMany(ch => ch.Chats)
                .HasForeignKey(c => c.AttachedToId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(c => c.Listener)
                .WithMany()
                .HasForeignKey(c => c.ListenerId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
