using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trelo1.Interfaces;
using TreloDAL.Data;
using TreloDAL.Models;

namespace TreloBLL.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TreloDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TreloDbContext>())
                {
                    //add admin user
                    if (!context.Users.Any())
                    {
                        var _userService = serviceScope.ServiceProvider.GetService<IUserService>();

                        var adminRole = context.Roles.FirstOrDefault(p => p.RoleName == "Admin");
                        if(adminRole == null)
                        {
                            adminRole = new Role()
                            {
                                RoleName = "Admin"
                            };

                            context.Roles.Add(adminRole);
                        }
                        

                        var roles = new List<Role>() { adminRole };
                        var adminUser = new User
                        {
                            FullName = "Admin",
                            Password = _userService.HashUserPassword("admin123"),
                            Email = "admin@admin.com",
                            Role = roles,
                        };
                        context.Users.Add(adminUser);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
