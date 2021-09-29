using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    public class UserTask
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        [ForeignKey("AssignedUserId")]
        public int? AssignedUserId { get; set; }
        public User AssignedUser { get; set; }

        public Status Status { get; set; }

        [ForeignKey("BoardId")]
        public int BoardId { get; set; }
        public Board Board { get; set; }
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
