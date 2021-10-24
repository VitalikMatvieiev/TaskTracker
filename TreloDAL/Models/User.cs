using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreloDAL.Models
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

        public List<Role> Role { get; set; }
        
        public IList<Board> Boards { get; set; }
        public IList<UserTask> UserTasks { get; set; }
        public IList<RefreshToken> RefreshTokens { get; set; }
        public string Avatar { get; set; }
    }
}
