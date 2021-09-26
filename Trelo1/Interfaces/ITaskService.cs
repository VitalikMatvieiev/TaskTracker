using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trelo1.Models;

namespace Trelo1.Interfaces
{
    public interface ITaskService
    {
        void Create(UserTask userTask);
        void Delete(int id);
        void AssignUserToTask(int taskId, int userId);
        UserTask GetTask(int taskId);
        IEnumerable<UserTask> GetUserTasks(int userId);
        IEnumerable<UserTask> GetBoardTasks(int boardId);
        IEnumerable<UserTask> GetOrganizationTasks(int organizationId);
    }
}
