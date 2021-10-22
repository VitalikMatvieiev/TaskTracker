using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.DtoModel;


namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpPost]
        [Route("api/org/")]
        public IActionResult CreateOrg(OrganiztionDto organization)
        {
            _organizationService.CreateOrganization(organization);
            return Ok();
        }
        [HttpDelete]
        [Route("api/org/{orgId}")]
        public IActionResult DeleteOrg(int orgId)
        {
            _organizationService.DeleteOrganization(orgId);
            return Ok();
        }
        [HttpPost]
        [Route("api/org/{orgId}/board/{boardId}/")]
        public IActionResult ChangeOganizationForBoard(int boardId, int orgId)
        {
            _organizationService.AddBoardToOrg(boardId, orgId);
            return Ok();
        }
    }
}
