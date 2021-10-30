using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class TestLogEntityForLog1EntityTypeConfiguration : IEntityTypeConfiguration<TestLogEntityForLog1>
    {
        public void Configure(EntityTypeBuilder<TestLogEntityForLog1> builder)
        {
            builder.ToTable("TestLogEntityForLog1").HasKey(p => p.Id);
            builder.HasOne(p => p.UserTask).WithMany().HasForeignKey(p => p.EntityId);

                
        }
    }
    public class TestLogEntityForLog2EntityTypeConfiguration : IEntityTypeConfiguration<TestLogEntityForLog2>
    {
        public void Configure(EntityTypeBuilder<TestLogEntityForLog2> builder)
        {
            builder.ToTable("TestLogEntityForLog2").HasKey(p => p.Id);
            builder.HasOne(p => p.UserTask).WithMany().HasForeignKey(p=>p.EntityId);


        }
    }
}