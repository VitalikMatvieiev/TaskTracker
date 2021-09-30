using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data;
using Trelo1.Interfaces;

using TreloDAL.UnitOfWork;
using TreloDAL.Models;

namespace Trelo1.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(User user)
        {
            if(user != null)
            {
                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            if(userId != 0)
            {
                var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
                _unitOfWork.Users.Remove(user);
                _unitOfWork.SaveChanges();
            }
        }

        public IList<User> GetAllUsers()
        {
            List<User> users = _unitOfWork.Users.ToList();
            return users;
        }

        public IList<User> GetUserInBoard(int boadrdId)
        {
            if(boadrdId != 0)
            {
                var usersInBoard = _unitOfWork.Boards.FirstOrDefault(b=>b.Id == boadrdId, includeProperties: "Users").Users;
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
                var boardInOrganization = _unitOfWork.Organizations.FirstOrDefault(o => o.Id == organizationId, includeProperties: "Boards").Boards.Where(u => u.Users != null);
                List <User> users = new List<User>();
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
