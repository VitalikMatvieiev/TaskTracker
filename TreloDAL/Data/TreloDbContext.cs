using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data.Configuration;
using TreloDAL.Models;

namespace TreloDAL.Data
{
    public class TreloDbContext : DbContext
    {
        public TreloDbContext(DbContextOptions<TreloDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTaskEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BoardEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TaskFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AllowedFileTypesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TaskChangesLogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CodeMigrationEntityTypeConfiguration());
        }

        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations  { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskFile> TaskFiles { get; set;}
        public DbSet<AllowedFileTypes> AllowedFileTypes { get; set; }
        public DbSet<TaskChangesLog> TaskChangesLogs { get; set; }
        public DbSet<UserCredentialFile> UserCredentialFiles { get; set; }
        public DbSet<CodeMigration> CodeMigrations { get; set; }
    }
}
