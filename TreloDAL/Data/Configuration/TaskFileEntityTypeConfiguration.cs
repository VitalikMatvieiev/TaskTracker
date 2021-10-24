using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace TreloDAL.Data.Configuration
{
    public class TaskFileEntityTypeConfiguration : IEntityTypeConfiguration<TaskFile>
    {
        public void Configure(EntityTypeBuilder<TaskFile> builder)
        {
            builder.ToTable("TaskFile").HasKey(p => p.DocumentId);
            builder.HasOne(p => p.UserTask).WithMany(p => p.TaskFiles).HasForeignKey(p => p.TaskId);
        }
    }
}