using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using Trelo1.Models;
using Trelo1.Models.ViewModel;

namespace Trelo1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly MapperConfiguration config;
        private readonly Mapper mapper;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
            config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserTask, TaskViewModel>()
                .ForMember("AssignedUserId", opt => opt.MapFrom(c => c.AssignedUser.Id));
            });
            config.CreateMapper();
            mapper = new Mapper(config);
        }
        [HttpPost]
        public IActionResult CreateTask(UserTask userTask)
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
        public IEnumerable<TaskViewModel> GetBoardTasks(int boardId)
        {
            IEnumerable<UserTask> tasks = _taskService.GetBoardTasks(boardId);
            IEnumerable<TaskViewModel> tasksVM = mapper.Map<IEnumerable<TaskViewModel>>(tasks);
            return tasksVM;
        }
        
        [HttpGet]
        public IEnumerable<TaskViewModel> GetOrganizationTasks(int organizationId)
        {
            IEnumerable<UserTask> tasks = _taskService.GetOrganizationTasks(organizationId);
            IEnumerable<TaskViewModel> tasksVM = mapper.Map<IEnumerable<TaskViewModel>>(tasks);
            return tasksVM;
        }
        
        [HttpGet]
        public TaskViewModel GetTask(int taskId)
        {
            var task = _taskService.GetTask(taskId);
            var taskVM = mapper.Map<TaskViewModel>(task);

            return taskVM;
        }

        [HttpGet]
        public IList<TaskViewModel> GetUserTasks(int userId)
        {
            var tasks = _taskService.GetUserTasks(userId)?.ToList();
            var tasksVM = mapper.Map<List<TaskViewModel>>(tasks);
            return tasksVM;
        }
        [HttpPut]
        public IActionResult AssignUserToTask(int taskId, int userId)
        {
            _taskService.AssignUserToTask(taskId, userId);
            return Ok();
        }

    }
}
