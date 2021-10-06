using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data;
using Trelo1.Interfaces;

using TreloDAL.UnitOfWork;
using TreloDAL.Models;
using TreloBLL.DtoModel;
using AutoMapper;

namespace Trelo1.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(UserDto userDto)
        {
            if(userDto != null)
            {
                var user = _mapper.Map<User>(userDto);
                _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
            }
        }

        public bool DeleteUser(int userId)
        {
            if(userId != 0)
            {
                var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    _unitOfWork.Users.Remove(user);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public IList<UserDto> GetAllUsers()
        {
            List<User> users = _unitOfWork.Users.ToList();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public IList<UserDto> GetUserInBoard(int boadrdId)
        {
            if(boadrdId != 0)
            {
                var usersInBoard = _unitOfWork.Boards.FirstOrDefault(b=>b.Id == boadrdId, includeProperties: "Users").Users;
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
                var boardInOrganization = _unitOfWork.Organizations.FirstOrDefault(o => o.Id == organizationId, includeProperties: "Boards").Boards.Where(u => u.Users != null);
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
