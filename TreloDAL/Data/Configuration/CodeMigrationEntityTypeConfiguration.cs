using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class CodeMigrationEntityTypeConfiguration : IEntityTypeConfiguration<CodeMigration>
    {
        public void Configure(EntityTypeBuilder<CodeMigration> builder)
        {
            builder.ToTable("CodeMigration").HasKey(p => p.Id);
            builder.HasIndex(p=>p.MigrationName).IsUnique();
            builder.Property(p => p.MigrationName).IsRequired();
            builder.Property(p => p.HasRun).IsRequired();

        }
    }
}