using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Data;
using Trelo1.Interfaces;
using Trelo1.Models;

namespace Trelo1.Services
{
    public class TaskService : ITaskService
    {
        private readonly TreloDbContext _db;
        public TaskService(TreloDbContext db)
        {
            _db = db;
        }
        public void AssignUserToTask(int taskId, int userId)
        {
            var task = _db.Tasks.Include(u=>u.AssignedUser).FirstOrDefault(u => u.Id == taskId);
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            task.AssignedUser = user;
            _db.SaveChanges();
        }

        public void Create(UserTask userTask)
        {
            if(userTask != null)
            {
                _db.Tasks.Add(userTask);
                _db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            }
        }

        public IEnumerable<UserTask> GetBoardTasks(int boardId)
        {
            if(boardId != 0)
            {
                var boardTask = _db.Boards.FirstOrDefault(b => b.Id == boardId).UserTasks;
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
                var boardInOrganization = _db.Organizations.Include(u => u.Boards).FirstOrDefault(o => o.Id == organizationId).Boards.Where(u => u.UserTasks != null);
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
                var task = _db.Tasks.FirstOrDefault(t => t.Id == taskId);
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
                var taskList = _db.Users.Include(u=>u.UserTasks).FirstOrDefault(t => t.Id == userId).UserTasks;
                return taskList;
            }
            else
            {
                return null;
            }
        }
    }
}
