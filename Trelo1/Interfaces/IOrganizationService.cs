using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Models;

namespace Trelo1.Interfaces
{
    public interface IOrganizationService
    {
        void CreateOrganization(Organization organization);
        void DeleteOrganization(int organizationId);
/*        void AddUserToOrganization(int userid, int boardId);*/
/*        void DeleteUserFromOrganization(int userId, int organizationId);*/
    }
}
