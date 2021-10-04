using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class BoardEntityTypeConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.ToTable("Boards").HasKey(p => p.Id);
            builder.HasOne(p => p.Organization).WithMany(p => p.Boards).HasForeignKey(p => p.OrganizationId);
        }
    }
}