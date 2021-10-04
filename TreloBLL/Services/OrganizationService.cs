using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data;
using Trelo1.Interfaces;

using TreloDAL.UnitOfWork;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;

namespace Trelo1.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrganizationService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateOrganization(OrganiztionDto organizationDto)
        {
            if(organizationDto != null)
            {
                var organization = _mapper.Map<Organization>(organizationDto);
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
