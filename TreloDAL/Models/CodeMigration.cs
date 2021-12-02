using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class CodeMigration
    {
        public int Id { get; set; }
        public string MigrationName { get; set; }
        public bool HasRun { get; set; }
    }
}
