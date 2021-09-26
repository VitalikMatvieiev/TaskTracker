using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;

namespace Trelo1.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly TreloDbContext _db;
        public OrganizationService(TreloDbContext db)
        {
            db = _db;
        }
/*        public void AddUserToOrganization(int userid, int boardId)
        {
            
        }*/

        public void CreateOrganization(Organization organization)
        {
            if(organization != null)
            {
                _db.Organizations.Add(organization);
                _db.SaveChanges();
            }
        }

        public void DeleteOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var organization = _db.Organizations.FirstOrDefault(o => o.Id == organizationId);
                if(organization != null)
                {
                    _db.Organizations.Remove(organization);
                    _db.SaveChanges();
                }
            }
        }

/*        public void DeleteUserFromOrganization(int userId, int organizationId)
        {
            if(userId != 0)
            {
                var organizationBoards = _db.Organizations.FirstOrDefault(o => o.Id == organizationId).Boards;
                var userBoard = _db.Users.FirstOrDefault(u => u.Id == userId).Boards.;
                var result = organizationBoards.Except(userBoard);
                if(result.Count() == 0 || result.Count()<.)
            }
        }*/
    }
}
