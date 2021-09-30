using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class UserTaskEntityTypeConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.ToTable("Tasks").HasKey(p => p.Id);
            builder.HasOne(p => p.Board).WithMany(p => p.UserTasks).HasForeignKey(p => p.BoardId);
            builder.HasOne(p => p.AssignedUser).WithMany(p => p.UserTasks).HasForeignKey(p => p.AssignedUserId);
            builder.Property(p => p.Name).IsRequired();
                
        }
    }
}