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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public IActionResult CreateTask(TaskDto userTask)
        {
            _taskService.Create(userTask);
            return Ok();
        }
        
        [HttpDelete]
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
        public async Task<IEnumerable<TaskDto>> GetBoardTasks(int boardId)
        {
            IEnumerable<TaskDto> tasks = await _taskService.GetBoardTasks(boardId);
            return tasks;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<TaskDto> GetOrganizationTasks(int organizationId)
        {
            IEnumerable<TaskDto> tasks = _taskService.GetOrganizationTasks(organizationId);
            return tasks;
        }
        
        [HttpGet]
        public async Task<TaskDto> GetTask(int taskId)
        {
            var task = await _taskService.GetTask(taskId);

            return task;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IList<TaskDto>> GetUserTasks(int userId)
        {
            var tasks = await _taskService.GetUserTasks(userId);

            return tasks?.ToList();
        }
        [HttpPut]
        public async Task<IActionResult> AssignUserToTask(int taskId, int userId)
        {
            await _taskService.AssignUserToTask(taskId, userId);
            return Ok();
        }

    }
}
