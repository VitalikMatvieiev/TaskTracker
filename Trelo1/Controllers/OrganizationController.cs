using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.ClaimsPrincipalExtensions;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IAppAuthentication _appAuthentication;
        public OrganizationController(IOrganizationService organizationService, IAppAuthentication appAuthentication)
        {
            _organizationService = organizationService;
            _appAuthentication = appAuthentication;
        }
        [HttpPost]
        [Route("api/organizations/")]
        public IActionResult CreateOrg(OrganiztionDto organization)
        {
            _organizationService.CreateOrganization(organization);
            return Ok();
        }
        [HttpDelete]
        [Route("api/organizations/{orgId}")]
        public IActionResult DeleteOrg(int orgId)
        {
            var curentUserId = User.GetUserId();
            if (_appAuthentication.HasOrganizationAsses(curentUserId, orgId))
            {
                _organizationService.DeleteOrganization(orgId);
                return Ok();
            }

            return StatusCode(401, "You haven't assess to this Org");
        }
        [HttpPut]
        [Route("api/organizations/{orgId}/boards/{boardId}/organizations")]
        public IActionResult ChangeOganizationForBoard(int boardId, int orgId)
        {
            var curentUserId = User.GetUserId();
            if (_appAuthentication.HasOrganizationAsses(curentUserId, orgId))
            {
                _organizationService.AddBoardToOrg(boardId, orgId); 
                return Ok();
            }

            return StatusCode(401, "You haven't assess to this Org");
        }
    }
}
