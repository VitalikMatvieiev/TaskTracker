using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models.ViewModel
{
    public class TaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
