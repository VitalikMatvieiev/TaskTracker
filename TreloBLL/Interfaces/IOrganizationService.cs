using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TreloDAL.Models;

namespace Trelo1.Interfaces
{
    public interface IOrganizationService
    {
        void CreateOrganization(Organization organization);
        void DeleteOrganization(int organizationId);
        void AddBoardToOrg(int boardId, int orgId);
    }
}
