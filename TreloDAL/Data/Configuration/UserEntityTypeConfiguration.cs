using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
/*            builder.HasOne(p => p.Role).WithMany(p => p.Users).HasForeignKey(p => p.RoleId); ;*/
            builder.Property(p => p.Email).IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(p => p.Password).IsRequired();

        }
    }
}