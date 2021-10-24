using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreloDAL.Models
{
    public class UserTask
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }


        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }

        public Status Status { get; set; }


        public int BoardId { get; set; }
        public Board Board { get; set; }

        public IList<TaskFile> Tasks { get; set; }
    }
    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }
    public enum Status
    {
        ToDo,
        InProgress,
        Done
    }
}
