using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Character");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .HasMaxLength(400);

            builder.Property(c => c.Status)
                .HasDefaultValue(CharacterStatus.None)
                .HasConversion(new EnumToStringConverter<CharacterStatus>())
                .IsRequired();

        }
    }
}
