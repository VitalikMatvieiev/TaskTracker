using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Trelo1.Interfaces;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;
using TreloDAL.Data;
using System.Threading.Tasks;

namespace Trelo1.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrganizationService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrganization(OrganiztionDto organizationDto)
        {
            if(organizationDto != null)
            {
                var organization = _mapper.Map<Organization>(organizationDto);
                _dbContext.Organizations.Add(organization);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteOrganization(int organizationId)
        {
            if (organizationId != 0)
            {
                var organization = await _dbContext.Organizations.FirstOrDefaultAsync(o => o.Id == organizationId);
                if(organization != null)
                {
                    _dbContext.Organizations.Remove(organization);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task AddBoardToOrg(int boardId, int orgId)
        {
            if(boardId != 0 && orgId != 0)
            {
                var organization = await _dbContext.Organizations.Include(p=>p.Boards).FirstOrDefaultAsync(o => o.Id == orgId);
                var board = _dbContext.Boards.FirstOrDefault(b => b.Id == boardId);
                if (organization != null && board != null)
                {
                    organization.Boards.Add(board);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
