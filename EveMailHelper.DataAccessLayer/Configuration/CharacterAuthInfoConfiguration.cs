using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using EveMailHelper.DataModels;

namespace EveNatTools.DataAccessLibrary.Configuration
{
    public class CharacterAuthInfoConfiguration : IEntityTypeConfiguration<CharacterAuthInfo>
    {
        public void Configure(EntityTypeBuilder<CharacterAuthInfo> builder)
        {
            // id is the only property required, because it is used in the initial 
            // oauth request to the oauth provider in order to obtain the 
            // accesstoken
            // * so the rest is not yet known at the time of authentication to the oauth provider *
            builder
                .Property(c => c.Id)
                .IsRequired()
                .HasComment("random number for added security");

            builder
                .Property(c => c.AccessToken)                
                .HasMaxLength(2000)
                .HasComment("oauth accesstoken");

            builder
                .Property(c => c.RefreshToken)
                .HasMaxLength(1536)              
                .HasComment("oauth refreshtoken");

            builder
                .Property(c => c.TokenType)
                .HasMaxLength(128)
                .HasComment("??");

            builder.HasOne(cai => cai.Char)
                .WithMany()
                .HasForeignKey(cai => cai.CharId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder
                .Property(c => c.Scopes)
                // convert the list of strings to a string delimited by a special character
                .HasConversion(
                    new ValueConverter<IList<string>, string>(
                        v => string.Join(';', v), v => v.Split(new[] { ';' })
                    ),
                    new ValueComparer<IList<string>>(
                        (c1, c2) => new HashSet<string>(c1!).SetEquals(new HashSet<string>(c2!)),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                    );
        }
    }
}
