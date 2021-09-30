using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data.Configuration;
using Trelo1.Models;

namespace Trelo1.Data
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
        }

        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations  { get; set; }
    }
}
