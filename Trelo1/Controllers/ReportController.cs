using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult GetCsvUserTaskReport(int userId)
        {
            var report = _reportService.GenereteUserTasksReport(userId);
            var date = DateTime.Now;
            return File(Encoding.UTF8.GetBytes(report), "text/csv", $"user_{userId}_tasks_{date}.csv");
        }

        [HttpGet]
        public IActionResult GetCsvBoardTaskReport(int boardId)
        {
            var report = _reportService.GenereteBoardTasksReport(boardId);
            var date = DateTime.Now;
            return File(Encoding.UTF8.GetBytes(report), "text/csv", $"board_{boardId}_tasks_{date}.csv");
        }
    }
}
