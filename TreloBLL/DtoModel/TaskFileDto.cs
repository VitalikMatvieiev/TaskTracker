using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.DtoModel
{
    public class TaskFileDto
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }

        public int TaskId { get; set; }
    }
}
