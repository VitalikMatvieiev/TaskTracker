using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.Interfaces;
using TreloDAL.Data;

namespace TreloBLL.CodeMigration
{
    public class ChangeUserPasswordFormStringToHash : ICodeMigration
    {
        private readonly IServiceProvider _serviceProvider;
        public ChangeUserPasswordFormStringToHash(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Run()
        {
            var x = _serviceProvider.CreateScope();
            var _userService = x.ServiceProvider.GetService<IUserService>();
            var _dbContext = x.ServiceProvider.GetService<TreloDbContext>();
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                user.Password = _userService.HashUserPassword(user.Password);
                _dbContext.Update(user);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
