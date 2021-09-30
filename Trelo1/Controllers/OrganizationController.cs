using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;

using TreloDAL.Models;

namespace Trelo1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpPost]
        public IActionResult CreateOrg(Organization organization)
        {
            _organizationService.CreateOrganization(organization);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteOrg(int orgId)
        {
            _organizationService.DeleteOrganization(orgId);
            return Ok();
        }
        [HttpPost]
        public IActionResult ChangeOganizationForBoard(int boardId, int orgId)
        {
            _organizationService.AddBoardToOrg(boardId, orgId);
            return Ok();
        }
    }
}
