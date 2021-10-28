using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class AllowedFileTypes
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public double AllowedSize { get; set; }
        public bool IsAllowed { get; set; }

    }
}
