using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public IList<Board> Boards { get; set; }
        public IList<UserTask> UserTasks { get; set; }
    }
}
