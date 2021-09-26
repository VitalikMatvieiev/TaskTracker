using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    [Keyless]
    public class UsersTasks
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int UserTaskId { get; set; }
        public UserTask UserTask { get; set; }

    }
}
