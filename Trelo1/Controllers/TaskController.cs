using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.ClaimsPrincipalExtensions;
using TreloBLL.DtoModel;


namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }
        [HttpPost]
        [Route("/api/tasks/")]
        public async Task<IActionResult> CreateTask([FromForm]string userTask, [FromForm]IList<IFormFile> formFilesm)
        {
            var userId = User.GetUserId();
            var userTaskObj = JsonSerializer.Deserialize<TaskDto>(userTask);
            await _taskService.Create(userTaskObj, formFilesm, null);
            return Ok();
        }

/*        [HttpPost]
        [Route("/api/tasks/")]
        public IActionResult CreateTask(TaskDto userTask)
        {
            _taskService.Create(userTask);
            return Ok();
        }*/

        [HttpDelete]
        [Route("/api/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            bool hasDeleted = await _taskService.Delete(taskId);
            if(hasDeleted)
            {
                return Ok();
            }

            return NoContent();
            
        }
        
        [HttpGet]
        [Route("/api/boards/{boardId}/tasks/")]
        public async Task<IEnumerable<TaskDto>> GetBoardTasks(int boardId)
        {
            IEnumerable<TaskDto> tasks = await _taskService.GetBoardTasks(boardId);
            return tasks;
        }
        
        [HttpGet]
        [Route("/api/organizations/{organizationId}/tasks/")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<TaskDto> GetOrganizationTasks(int organizationId)
        {
            IEnumerable<TaskDto> tasks = _taskService.GetOrganizationTasks(organizationId);
            return tasks;
        }
        
        [HttpGet]
        [Route("/api/tasks/{taskId}")]
        public async Task<TaskDto> GetTask(int taskId)
        {
            var task = await _taskService.GetTask(taskId);

            return task;
        }

        [HttpGet]
        [Route("/api/users/tasks/")]
        [Authorize(Roles = "Admin")]
        public async Task<IList<TaskDto>> GetUserTasks(SingleModel<int> userId)
        {
            IEnumerable<TaskDto> tasks;
            
            if(userId.Value == 0)
            {
                var userEmail = User.Identity.Name;
                var user = await _userService.GetUserData(userEmail);
                tasks = await _taskService.GetUserTasks(user.Id);
            } 
            else
            {
                tasks = await _taskService.GetUserTasks(userId.Value);
            }

            return tasks?.ToList();
        }

        [HttpPut]
        [Route("/api/users/tasks/{taskId}/assign/")]
        public async Task<IActionResult> AssignUserToTask(int taskId, SingleModel<int> userId)
        {
            if(userId.Value == 0)
            {
                var userEmail = User.Identity.Name;
                var user = await _userService.GetUserData(userEmail);
                await _taskService.AssignUserToTask(taskId, user.Id);
                return Ok();
            }
            else
            {
                await _taskService.AssignUserToTask(taskId, userId.Value);
                return Ok();
            }
        }

/*        [HttpPost]
        [Route("api/tasks/{taskId}/upload-file")]
        public async Task<IActionResult> AddFileToTaks(int taskId, IList<IFormFile> formFile)
        {
            await _taskService.AssigneFileToTask(formFile, taskId);
            return Ok();   
        }*/
    }
}
