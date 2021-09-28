using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Models;

namespace Trelo1.Data
{
    public class TreloDbContext : DbContext
    {
        public TreloDbContext(DbContextOptions<TreloDbContext> options) : base(options)
        {

        }

        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations  { get; set; }
    }
}
