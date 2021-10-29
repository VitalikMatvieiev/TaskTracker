using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.Interfaces
{
    public interface IAppAuthentication
    {
        bool HasTaskAsses(int userId, int taskId);
        bool HasBoardAsses(int userId, int boardId);
        bool HasOrganizationAsses(int userId, int ogrId);

    }
}
