using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class TaskFile
    {

        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] DataFiles { get; set; }

        public int TaskId { get; set; }
        public UserTask UserTask { get; set; }

    }
}
