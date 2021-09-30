using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class TestModelv : DbContext
    {
        public int ID { get; set; }
    }
}
