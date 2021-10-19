using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Trelo1.Interfaces;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;
using TreloDAL.Data;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;

        public TaskService(TreloDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void AssignUserToTask(int taskId, int userId)
        {
            var task = _dbContext.Tasks.Include(p=>p.AssignedUser).FirstOrDefault(u => u.Id == taskId);
            var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId).Result;
            task.AssignedUser = user;
            _dbContext.SaveChangesAsync();
        }

        public void Create(TaskDto userTaskDto)
        {
            if(userTaskDto != null)
            {
                var userTask = _mapper.Map<UserTask>(userTaskDto);
                _dbContext.Tasks.Add(userTask);
                _dbContext.SaveChangesAsync();
            }
        }

        public bool Delete(int id)
        {
            if (id != 0)
            {
                var task = _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id).Result;
                if(task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    _dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<TaskDto> GetBoardTasks(int boardId)
        {
            if(boardId != 0)
            {
                var boardTask = _dbContext.Boards.Include(p=>p.UserTasks).FirstOrDefaultAsync(b => b.Id == boardId).Result.UserTasks;
                var boardTaskDto = _mapper.Map<List<TaskDto>>(boardTask);
                return boardTaskDto;
            } 
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskDto> GetOrganizationTasks(int organizationId)
        {
            if (organizationId != 0)
            {
                var boardInOrganization = _dbContext.Boards.Include(p=>p.UserTasks).Where(o => o.OrganizationId == organizationId);
                
                List<UserTask> tasks = new List<UserTask>();
                foreach (var board in boardInOrganization)
                {
                    tasks.AddRange(board.UserTasks);
                }

                var taskDto = _mapper.Map<List<TaskDto>>(tasks);
                return taskDto;
            }
            else
            {
                return null;
            }
        }

        public TaskDto GetTask(int taskId)
        {
            if(taskId != 0)
            {
                var task = _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId).Result;
                var taskDto = _mapper.Map<TaskDto>(task);
                return taskDto;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TaskDto> GetUserTasks(int userId)
        {
            if (userId != 0)
            {
                var taskList = _dbContext.Users.Include(p => p.UserTasks).FirstOrDefaultAsync(t => t.Id == userId).Result.UserTasks;
                var tasksDto = _mapper.Map<List<TaskDto>>(taskList);
                return tasksDto;
            }
            else
            {
                return null;
            }
        }
    }
}
