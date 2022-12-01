using EveMailHelper.DataModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailRecipientCharacterConfiguration : IEntityTypeConfiguration<MailRecipientCharacter>
    {
        public void Configure(EntityTypeBuilder<MailRecipientCharacter> builder)
        {
            builder.HasBaseType<MailRecipient>();

            builder.HasOne(x => x.Character)
                .WithMany()
                .HasForeignKey(x => x.CharacterId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
