using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.Interfaces
{
    public interface IReportService
    {
        string GenereteUserTasksReport(int userId);
        string GenereteBoardTasksReport(int boardId);
    }
}
