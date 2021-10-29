using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.ClaimsPrincipalExtensions;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class FileTaskController : ControllerBase
    {
        private readonly ITaskFileService _taskFileService;
        private readonly IAppAuthentication _appAuthentication;
        public FileTaskController(ITaskFileService taskFileService, IAppAuthentication appAuthentication)
        {
            _taskFileService = taskFileService;
            _appAuthentication = appAuthentication;
        }

        [HttpPost]
        [Route("/api/tasks/files/{fileId}")]
        public async Task<IActionResult> ChangeFileName(int fileId, [FromBody]string newFileName)
        {
            var curentUserId = User.GetUserId();
            var file = _taskFileService.GetFile(fileId);
            if (_appAuthentication.HasTaskAsses(curentUserId, file.TaskId))
            {
                await _taskFileService.ChangeFileName(fileId, newFileName);
                return Ok();
            }

            return StatusCode(401, "You haven't assess to this task");
        }

        [HttpDelete]
        [Route("api/tasks/{taskId}/files/{fileId}")]
        public async Task<IActionResult> DeleteFileFromTask(int taskId, int fileId)
        {
            var currentUserId = User.GetUserId();
            if(_appAuthentication.HasTaskAsses(currentUserId, taskId))
            {
                await _taskFileService.DeleteFileFromTask(taskId, fileId);
                return Ok();
            }

            return StatusCode(401, "You haven't assess to this task");
        }

        [HttpGet]
        [Route("api/file/{fileId}")]
        public IActionResult GetTaskFile(int fileId)
        {
            var file = _taskFileService.GetFile(fileId);
            var curentUserId = User.GetUserId();
            if (_appAuthentication.HasTaskAsses(curentUserId, file.TaskId))
            {
                return File(file.DataFiles, file.ContentType);
            }
            return StatusCode(401, "You haven't assess for this task");
            
        }
    }
}
