using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;
using TreloDAL.Models;

namespace Trelo1.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDto user);
        Task<bool> DeleteUser(int userId);
        Task<IList<UserDto>> GetUserInBoard(int boadrdId);
        Task<IList<UserDto>> GetUserInOrganization(int organizationId);
        IList<UserDto> GetAllUsers();
        Task<User> GetUserData(string Email);
        Task AddUserAvatar(int userId, IFormFile userAvatar);
        string HashUserPassword(string password);
        Task<bool> CheckUserHashPassword(string Email, string password);
    }
}
