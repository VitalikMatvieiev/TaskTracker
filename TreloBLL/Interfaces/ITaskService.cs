using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloBLL.DtoModel;
using TreloDAL.Models;

namespace Trelo1.Interfaces
{
    public interface ITaskService
    {
        void Create(TaskDto userTask);
        bool Delete(int id);
        void AssignUserToTask(int taskId, int userId);
        TaskDto GetTask(int taskId);
        IEnumerable<TaskDto> GetUserTasks(int userId);
        IEnumerable<TaskDto> GetBoardTasks(int boardId);
        IEnumerable<TaskDto> GetOrganizationTasks(int organizationId);
    }
}
