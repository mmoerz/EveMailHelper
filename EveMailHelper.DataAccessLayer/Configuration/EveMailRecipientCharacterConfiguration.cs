using EveMailHelper.DataModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailRecipientCharacterConfiguration : IEntityTypeConfiguration<EveMailRecipientCharacter>
    {
        public void Configure(EntityTypeBuilder<EveMailRecipientCharacter> builder)
        {
            builder.HasBaseType<EveMailRecipient>();
            //builder.Ignore(er => er.Name);
        }
    }
}
