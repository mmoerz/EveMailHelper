﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.DataAccessLayer.Configuration
{
    public class EveAccountConfiguration : IEntityTypeConfiguration<EveAccount>
    {
        public void Configure(EntityTypeBuilder<EveAccount> builder)
        {
            builder.ToTable("EveAccount","Security");

            builder.Property(ea => ea.Name)
                .HasMaxLength(200);
            
            builder.HasMany(a => a.Characters)
                .WithOne(c => c.EveAccount)
                .HasForeignKey(c => c.EveAccountId);
        }
    }
}
