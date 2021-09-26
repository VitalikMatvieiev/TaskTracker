using Microsoft.EntityFrameworkCore;
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
            _db = db;
        }

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
        public void AddBoardToOrg(int boardId, int orgId)
        {
            if(boardId != 0 && orgId != 0)
            {
                var organization = _db.Organizations.Include(o=>o.Boards).FirstOrDefault(o => o.Id == orgId);
                var board = _db.Boards.FirstOrDefault(b => b.Id == boardId);
                if (organization != null && board != null)
                {
                    organization.Boards.Add(board);
                    _db.SaveChanges();
                }
            }
        }
    }
}
