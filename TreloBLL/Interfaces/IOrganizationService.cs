using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;

namespace Trelo1.Interfaces
{
    public interface IOrganizationService
    {
        Task CreateOrganization(OrganiztionDto organization);
        Task<bool> DeleteOrganization(int organizationId);
        Task AddBoardToOrg(int boardId, int orgId);
    }
}
