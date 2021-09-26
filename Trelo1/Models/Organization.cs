using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trelo1.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Board> Boards { get; set;}
    }
}
