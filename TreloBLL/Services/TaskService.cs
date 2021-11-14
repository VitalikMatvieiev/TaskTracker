using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Trelo1.Interfaces;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;
using TreloDAL.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using TreloBLL.Interfaces;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private const int MaxFileCount = 5;
        private readonly TreloDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITaskFileService _taskFileService;
        private readonly IChangeTrackingService _changeTrackingService;

        public TaskService(TreloDbContext dbContext, IMapper mapper, 
            ITaskFileService taskFileService, 
            IChangeTrackingService changeTrackingService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _taskFileService = taskFileService;
            _changeTrackingService = changeTrackingService;
        }

        public async Task AssignUserToTask(int taskId, int userId)
        {
            var task = await _dbContext.Tasks.Include(p => p.AssignedUser).FirstOrDefaultAsync(u => u.Id == taskId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            task.AssignedUser = user;
            _changeTrackingService.TrackChangeGeneric<UserTask, TaskChangesLog>(task, taskId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(TaskDto userTaskDto, IList<IFormFile> formFiles, int? taskId)
        {
            if (taskId != null)
            {
                var task = await _dbContext.Tasks.Include(t => t.TaskFiles).AsNoTracking().FirstOrDefaultAsync(t => t.Id == taskId);

                if(task != null && formFiles != null)
                {
                    var files = _taskFileService.GenereteFilesForTask(formFiles);
                    if(task.TaskFiles.Count() + files.Count() > MaxFileCount)
                    {
                        return;
                    }

                    userTaskDto.TaskFiles.AddRange(files);
                    task = _mapper.Map<UserTask>(userTaskDto);
                    task.Id = taskId.Value;

                    _dbContext.Update(task);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (userTaskDto != null)
            {
                userTaskDto.TaskFiles = _taskFileService.GenereteFilesForTask(formFiles);
                var userTask = _mapper.Map<UserTask>(userTaskDto);
                _dbContext.Tasks.Add(userTask);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task != null)
                {
                    _dbContext.Tasks.Remove(task);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<TaskDto>> GetBoardTasks(int boardId)
        {
            if (boardId != 0)
            {
                var board = await _dbContext.Boards.Include(p => p.UserTasks).FirstOrDefaultAsync(b => b.Id == boardId);
                var taks = board.UserTasks;
                var boardTaskDto = _mapper.Map<List<TaskDto>>(taks);
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
                var boardInOrganization = _dbContext.Boards.Include(p => p.UserTasks).Where(o => o.OrganizationId == organizationId);

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

        public async Task<TaskDto> GetTask(int taskId)
        {
            if (taskId != 0)
            {
                var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                var taskDto = _mapper.Map<TaskDto>(task);
                return taskDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<TaskDto>> GetUserTasks(int userId)
        {
            if (userId != 0)
            {
                var user = await _dbContext.Users.Include(p => p.UserTasks).FirstOrDefaultAsync(t => t.Id == userId);
                var task = user.UserTasks;
                var tasksDto = _mapper.Map<List<TaskDto>>(task);
                return tasksDto;
            }
            else
            {
                return null;
            }
        }
    }
}
