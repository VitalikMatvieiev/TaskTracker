using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;
using TreloDAL.UnitOfWork;

namespace Trelo1.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly UnitOfWork _unitOfWork;
        public OrganizationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateOrganization(Organization organization)
        {
            if(organization != null)
            {
                _unitOfWork.Organizations.Create(organization);
                _unitOfWork.SaveChanges();
            }
        }

        public void DeleteOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var organization = _unitOfWork.Organizations.FirstOrDefault(o => o.Id == organizationId);
                if(organization != null)
                {
                    _unitOfWork.Organizations.Remove(organization);
                    _unitOfWork.SaveChanges();
                }
            }
        }
        public void AddBoardToOrg(int boardId, int orgId)
        {
            if(boardId != 0 && orgId != 0)
            {
                var organization = _unitOfWork.Organizations.FirstOrDefault(o => o.Id == orgId, includeProperties: "Boards");
                var board = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId);
                if (organization != null && board != null)
                {
                    organization.Boards.Add(board);
                    _unitOfWork.SaveChanges();
                }
            }
        }
    }
}
