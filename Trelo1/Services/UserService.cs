using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;

namespace Trelo1.Services
{
    public class UserService : IUserService
    {
        private readonly TreloDbContext _db;
        public UserService(TreloDbContext db)
        {
            _db = db;
        }
        public void Create(User user)
        {
            if(user != null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            if(userId != 0)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }

        public IList<User> GetAllUsers()
        {
            List<User> users = _db.Users.ToList();
            return users;
        }

        public IList<User> GetUserInBoard(int boadrdId)
        {
            if(boadrdId != 0)
            {
                var usersInBoard = _db.Boards.Include(u => u.Users).FirstOrDefault(b=>b.Id == boadrdId).Users;
                return usersInBoard;
            }
            else
            {
                return null;
            }
        }

        public IList<User> GetUserInOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var boardInOrganization = _db.Organizations.Include(u => u.Boards).FirstOrDefault(o => o.Id == organizationId).Boards.Where(u => u.Users != null);
                List<User> users = new List<User>();
                foreach (var board in boardInOrganization)
                {
                    users.AddRange(board.Users);
                }
                return users;
            }
            else
            {
                return null;
            }
        }

    }
}
