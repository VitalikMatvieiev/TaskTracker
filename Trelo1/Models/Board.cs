using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        
        public List<UserTask> UserTasks { get; set; }
        
        public List<User>Users { get; set; }
    }
}
