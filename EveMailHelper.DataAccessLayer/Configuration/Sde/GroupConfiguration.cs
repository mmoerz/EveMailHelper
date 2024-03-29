﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataAccessLayer.Configuration.Sde
{
    public partial class GroupConfiguration
        : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(nameof(Group), Constants.SCHEMA_SDE);
            builder.HasKey(a => a.EveId);

            builder.Property(a => a.EveId)
                .HasMaxLength(Constants.SIZE_TEXT)
                .ValueGeneratedNever();

            builder.HasIndex(e => e.CategoryId);

            builder.Property(a => a.GroupName)
                .HasMaxLength(Constants.SIZE_TEXT)
                .IsRequired(true);

            builder.HasOne(d => d.Category)
                .WithMany(p => p.InvGroups)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(a => a.Icon)
                .WithMany(p => p.InvGroups)
                .HasForeignKey(a => a.IconId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
