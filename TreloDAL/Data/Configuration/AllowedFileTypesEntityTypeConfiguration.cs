using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class AllowedFileTypesEntityTypeConfiguration : IEntityTypeConfiguration<AllowedFileTypes>
    {
        public void Configure(EntityTypeBuilder<AllowedFileTypes> builder)
        {
            builder.ToTable("AllowedFileTypes").HasKey(p => p.Id);
        }
    }
}