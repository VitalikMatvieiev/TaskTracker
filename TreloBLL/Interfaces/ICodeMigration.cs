using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreloBLL.Interfaces
{
    public interface ICodeMigration
    {
        Task Run();
    }
}
