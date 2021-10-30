using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class TaskChangesLog
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public UserTask UserTask { get; set; }
        public string ChangeData { get; set; }
        public DateTimeOffset ChangeTime { get; set; }

    }
}
