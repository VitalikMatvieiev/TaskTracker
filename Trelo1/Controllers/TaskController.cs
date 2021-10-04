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
        public IActionResult DeleteTask(int taskId)
        {
            _taskService.Delete(taskId);
            return Ok();
        }
        
        [HttpGet]
        public IEnumerable<TaskDto> GetBoardTasks(int boardId)
        {
            IEnumerable<TaskDto> tasks = _taskService.GetBoardTasks(boardId);
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
        public TaskDto GetTask(int taskId)
        {
            var task = _taskService.GetTask(taskId);

            return task;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IList<TaskDto> GetUserTasks(int userId)
        {
            var tasks = _taskService.GetUserTasks(userId)?.ToList();

            return tasks;
        }
        [HttpPut]
        public IActionResult AssignUserToTask(int taskId, int userId)
        {
            _taskService.AssignUserToTask(taskId, userId);
            return Ok();
        }

    }
}
