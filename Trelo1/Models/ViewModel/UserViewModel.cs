using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models.ViewModel
{
    public class UserViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public IList<Board> Boards { get; set; }
        public IList<UserTask> UserTasks { get; set; }
    }
}
