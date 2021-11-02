using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreloBLL.Interfaces;
using TreloDAL.Data;

namespace TreloBLL.Services
{
    public class AppAuthentication : IAppAuthentication
    {
        private readonly TreloDbContext _dbContext;
        public AppAuthentication(TreloDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool HasBoardAsses(int userId, int boardId)
        {
            var user = _dbContext.Users.Include(u => u.Boards).FirstOrDefaultAsync(u => u.Id == userId && u.Boards.Any(b => b.Id == boardId));
            return user == null ? false : true;
        }

        public bool HasOrganizationAsses(int UserId, int orgId)
        {
            var org = _dbContext.Organizations.Include(o => o.Boards).FirstOrDefault(o => o.Id == orgId);
            var user = _dbContext.Users.Include(u => u.Boards).FirstOrDefault(u => u.Id == UserId);
            foreach(var board in user.Boards)
            {
                return org.Boards.Any(b => b.Id == board.Id) ? true : false;
            }

            return false;
        }

        public bool HasTaskAsses(int userId, int taskId)
        {
            var user = _dbContext.Users.Include(u => u.UserTasks).FirstOrDefault(u => u.Id == userId && u.UserTasks.Any(u => u.Id == taskId));
            return user == null ? false : true;
        }
    }
}
