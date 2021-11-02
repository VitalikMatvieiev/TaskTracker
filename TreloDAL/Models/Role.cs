using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
