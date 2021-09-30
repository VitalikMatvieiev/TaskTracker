using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    public class Board
    {
        public Board()
        {
            if (Users == null)
            {
                Users = new List<User>();
            }

            if (UserTasks == null)
            {
                UserTasks = new List<UserTask>();
            }
        }
        
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("OrganizationId")]
        [Required]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        
        public List<UserTask> UserTasks { get; set; }
        
        public List<User>Users { get; set; }
    }
}
