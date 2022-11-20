using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;

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
                .IsRequired(false)
                .HasMaxLength(8000);
            builder.Property(c => c.RecruitmentNote)
                .IsRequired(false)
                .HasMaxLength(400);
            builder.Property(c => c.Title)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(c => c.Status)
                .HasDefaultValue(CharacterStatus.None)
                .HasConversion(new EnumToStringConverter<CharacterStatus>())
                .IsRequired();

            //builder.HasOne(c => c.Corporation)
            //    .WithMany(co => co.Members)
            //    .HasForeignKey(c => c.CorporationId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
