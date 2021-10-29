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
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IAppAuthentication _appAuthentication;

        public TaskController(ITaskService taskService, IUserService userService, IAppAuthentication appAuthentication)
        {
            _taskService = taskService;
            _userService = userService;
            _appAuthentication = appAuthentication;
        }

        [HttpPost]
        [Route("/api/tasks/")]
        public async Task<IActionResult> CreateTask([FromForm]string userTask, [FromForm]IList<IFormFile> formFilesm)
        {
            var userTaskObj = JsonSerializer.Deserialize<TaskDto>(userTask);
            
            var currentUserId = User.GetUserId();

            if (_appAuthentication.HasBoardAsses(currentUserId, userTaskObj.BoardId))
            {
                await _taskService.Create(userTaskObj, formFilesm, null);
                return Ok();
            }

            return StatusCode(401, "You haven't asses to this board");
        }

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
            var currentUserId = User.GetUserId();
            if (_appAuthentication.HasBoardAsses(currentUserId, boardId))
            {
                IEnumerable<TaskDto> tasks = await _taskService.GetBoardTasks(boardId);
                return tasks;
            }
            return null;
        }
        
        [HttpGet]
        [Route("/api/organizations/{organizationId}/tasks/")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<TaskDto> GetOrganizationTasks(int organizationId)
        {
            var currentUserId = User.GetUserId();
            if (_appAuthentication.HasOrganizationAsses(currentUserId, organizationId))
            {
                IEnumerable<TaskDto> tasks = _taskService.GetOrganizationTasks(organizationId);
                return tasks;
            }
            return null;
        }
        
        [HttpGet]
        [Route("/api/tasks/{taskId}")]
        public async Task<TaskDto> GetTask(int taskId)
        {
            var curentUserId = User.GetUserId();
            if(_appAuthentication.HasTaskAsses(curentUserId, taskId))
            {
                var task = await _taskService.GetTask(taskId);
                return task;
            }
            return null;
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
    }
}
