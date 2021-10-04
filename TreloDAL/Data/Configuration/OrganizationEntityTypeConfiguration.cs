using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations").HasKey(p => p.Id);
        }
    }
}