using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreloBLL.ClaimsPrincipalExtensions;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IAppAuthentication _appAuthentication;

        public ReportController(IReportService reportService, IAppAuthentication appAuthentication)
        {
            _reportService = reportService;
            _appAuthentication = appAuthentication;
        }

        [HttpGet]
        [Route("api/users/report/")]
        public IActionResult GetCsvUserTaskReport(SingleModel<int> userId)
        {
            var report = _reportService.GenereteUserTasksReport(userId.Value);
            var date = DateTime.Now;
            return File(Encoding.UTF8.GetBytes(report), "text/csv", $"user_{userId.Value}_tasks_{date}.csv");
        }

        [HttpGet]
        [Route("api/boards/{boardId}/report/")]
        public IActionResult GetCsvBoardTaskReport(int boardId)
        {
            var currentUserId = User.GetUserId();
            if(_appAuthentication.HasBoardAsses(currentUserId, boardId))
            {
                var report = _reportService.GenereteBoardTasksReport(boardId);
                var date = DateTime.Now;
                return File(Encoding.UTF8.GetBytes(report), "text/csv", $"board_{boardId}_tasks_{date}.csv");
            }
            return StatusCode(401, "You haven't assess to this board");
        }
    }
}
