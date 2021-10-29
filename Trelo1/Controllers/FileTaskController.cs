using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class FileTaskController : ControllerBase
    {
        private readonly ITaskFileService _taskFileService;
        public FileTaskController(ITaskFileService taskFileService)
        {
            _taskFileService = taskFileService;
        }

        [HttpPost]
        [Route("/api/tasks/files/{fileId}")]
        public async Task<IActionResult> ChangeFileName(int fileId, [FromBody]string newFileName)
        {
            await _taskFileService.ChangeFileName(fileId, newFileName);
            return Ok();
        }

        [HttpDelete]
        [Route("api/tasks/{taskId}/files/{fileId}")]
        public async Task<IActionResult> DeleteFileFromTask(int taskId, int fileId)
        {
            await _taskFileService.DeleteFileFromTask(taskId, fileId);
            return Ok();
        }

        [HttpGet]
        [Route("api/file/{fileId}")]
        public IActionResult GetTaskFile(int fileId)
        {
            var file = _taskFileService.GetFile(fileId);

            return File(file.ByteArr, file.Format);
        }
    }
}
