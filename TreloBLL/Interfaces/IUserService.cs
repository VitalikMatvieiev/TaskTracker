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
        void Create(UserDto user);
        bool DeleteUser(int userId);
        IList<UserDto> GetUserInBoard(int boadrdId);
        IList<UserDto> GetUserInOrganization(int organizationId);
        IList<UserDto> GetAllUsers();
    }
}
