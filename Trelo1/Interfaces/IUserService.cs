using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Models;

namespace Trelo1.Interfaces
{
    public interface IUserService
    {
        void Create(User user);
        void DeleteUser(int userId);
        IList<User> GetUserInBoard(int boadrdId);
        IList<User> GetUserInOrganization(int organizationId);
        IList<User> GetAllUsers();
    }
}
