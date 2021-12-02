using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TreloDAL.Data;

namespace TreloBLL.Services
{
    public class CodeMigration
    {
        public Assembly[] GetAll()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
