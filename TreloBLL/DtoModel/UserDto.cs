using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreloDAL.Models;

namespace TreloBLL.DtoModel
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public IList<Board> Boards { get; set; }
        public IList<UserTask> UserTasks { get; set; }
    }
}
