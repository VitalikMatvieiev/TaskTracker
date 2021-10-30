using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class TestLogEntityForLog2
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public UserTask UserTask { get; set; }
        public string ChangeData { get; set; }
        public DateTimeOffset ChangeTime { get; set; }
    }
}
