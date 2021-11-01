using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class UserCredentialFileEntityTypeConfiguration : IEntityTypeConfiguration<UserCredentialFile>
    {
        public void Configure(EntityTypeBuilder<UserCredentialFile> builder)
        {
            builder.ToTable("UserCredentialFile").HasKey(p => p.FileId);
            builder.HasOne(p => p.User).WithMany(p => p.UserCredentialFiles);
            builder.Property(p => p.FileData).IsRequired();
            builder.Property(p => p.ContentType).IsRequired();

        }
    }
}