using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class TaskChangesLogEntityTypeConfiguration : IEntityTypeConfiguration<TaskChangesLog>
    {
        public void Configure(EntityTypeBuilder<TaskChangesLog> builder)
        {
            builder.ToTable("TaskChangesLogs").HasKey(p => p.Id);
            builder.HasOne(p => p.UserTask).WithMany().HasForeignKey(p => p.TaskId);
            builder.Property(p => p.ChangeData).IsRequired();
                
        }
    }
}