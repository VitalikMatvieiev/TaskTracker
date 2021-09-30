using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;
using TreloDAL.UnitOfWork;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _unitOfWork;
        public TaskService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AssignUserToTask(int taskId, int userId)
        {
            var task = _unitOfWork.UserTasks.FirstOrDefault(u => u.Id == taskId,includeProperties: "AssignedUser");
            var user = _unitOfWork.Users.FirstOrDefault(u => u.Id == userId);
            task.AssignedUser = user;
            _unitOfWork.SaveChanges();
        }

        public void Create(UserTask userTask)
        {
            if(userTask != null)
            {
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

        public IEnumerable<UserTask> GetBoardTasks(int boardId)
        {
            if(boardId != 0)
            {
                var boardTask = _unitOfWork.Boards.FirstOrDefault(b => b.Id == boardId, includeProperties: "UserTasks").UserTasks;
                return boardTask;
            } 
            else
            {
                return null;
            }
        }

        public IEnumerable<UserTask> GetOrganizationTasks(int organizationId)
        {
            if (organizationId != 0)
            {
                var boardInOrganization = _unitOfWork.Boards.GetAll(o => o.OrganizationId == organizationId, includeProperties: "UserTasks");
                List<UserTask> tasks = new List<UserTask>();
                foreach (var board in boardInOrganization)
                {
                    tasks.AddRange(board.UserTasks);
                }
                return tasks;
            }
            else
            {
                return null;
            }
        }

        public UserTask GetTask(int taskId)
        {
            if(taskId != 0)
            {
                var task = _unitOfWork.UserTasks.FirstOrDefault(t => t.Id == taskId);
                return task;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UserTask> GetUserTasks(int userId)
        {
            if (userId != 0)
            {
                var taskList = _unitOfWork.Users.FirstOrDefault(t => t.Id == userId, includeProperties: "UserTasks").UserTasks;
                return taskList;
            }
            else
            {
                return null;
            }
        }
    }
}
