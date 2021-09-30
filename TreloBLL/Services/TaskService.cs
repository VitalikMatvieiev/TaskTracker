using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Data;
using Trelo1.Interfaces;

using TreloDAL.UnitOfWork;
using TreloDAL.Models;
using AutoMapper;
using TreloBLL.DtoModel;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AssignUserToTask(int taskId, int userId)
        {
            var task = _unitOfWork.UserTasks.FirstOrDefault(u => u.Id == taskId,includeProperties: "AssignedUser");
            var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
            task.AssignedUser = user;
            _unitOfWork.SaveChanges();
        }

        public void Create(TaskDto userTaskDto)
        {
            if(userTaskDto != null)
            {
                var userTask = _mapper.Map<UserTask>(userTaskDto);
                _unitOfWork.UserTasks.Create(userTask);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                var task = _unitOfWork.UserTasks.FirstOrDefault(t => t.Id == id);
                _unitOfWork.UserTasks.Remove(task);
                _unitOfWork.SaveChanges();
            }
        }

        public IEnumerable<TaskDto> GetBoardTasks(int boardId)
        {
            if(boardId != 0)
            {
                var boardTask = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId, includeProperties: "UserTasks").UserTasks;
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
                var boardInOrganization = _unitOfWork.Boards.GetAll(o => o.OrganizationId == organizationId, includeProperties: "UserTasks");
                
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
                var task = _unitOfWork.UserTasks.FirstOrDefault(t => t.Id == taskId);
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
                var taskList = _unitOfWork.Users.FirstOrDefault(t => t.Id == userId, includeProperties: "UserTasks").UserTasks;
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
