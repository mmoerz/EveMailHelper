using EveMailHelper.DataModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public partial class EveMailRecipientConfiguration : IEntityTypeConfiguration<EveMailRecipient>
    {
        public void Configure(EntityTypeBuilder<EveMailRecipient> builder)
        {
            builder.HasKey(er => er.Id);
            builder.Ignore(er => er.Name);
            
        }
    }
}
