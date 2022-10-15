using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailSentToConfiguration : IEntityTypeConfiguration<EveMailSentTo>
    {
        public void Configure(EntityTypeBuilder<EveMailSentTo> builder)
        {
            builder.ToTable("EveMailSentTo");

            builder.HasOne(st => st.Character)
                .WithMany(c => c.EveMailReceived)
                .HasForeignKey(st => st.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(st => st.EveMail)
                .WithMany(em => em.SentTo)
                .HasForeignKey(st => st.EveMailId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
