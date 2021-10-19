using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TreloDAL.Data;
using Trelo1.Interfaces;
using TreloDAL.Models;
using TreloBLL.DtoModel;
using AutoMapper;

namespace Trelo1.Services
{
    public class UserService : IUserService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;


        public UserService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Create(UserDto userDto)
        {
            if(userDto != null)
            {
                var user = _mapper.Map<User>(userDto);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
        }

        public bool DeleteUser(int userId)
        {
            if(userId != 0)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public IList<UserDto> GetAllUsers()
        {
            List<User> users = _dbContext.Users.ToList();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public IList<UserDto> GetUserInBoard(int boadrdId)
        {
            if(boadrdId != 0)
            {
                var usersInBoard = _dbContext.Boards.Include(p=>p.Users).FirstOrDefault(b=>b.Id == boadrdId).Users;
                List<UserDto> userDtos = _mapper.Map<List<UserDto>>(usersInBoard);
                return userDtos;
            }
            else
            {
                return null;
            }
        }

        public IList<UserDto> GetUserInOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var boardInOrganization = _dbContext.Organizations.Include(p=>p.Boards).FirstOrDefault(o => o.Id == organizationId).Boards.Where(u => u.Users != null);
                List <User> users = new List<User>();
                foreach (var board in boardInOrganization)
                {
                    users.AddRange(board.Users);
                }

                List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
                return userDtos;
            }
            else
            {
                return null;
            }
        }

    }
}
