using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.DtoModel
{
    public class FileGeneralDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileType { get; set; }
        public byte[] DataFile { get; set; }
    }
}
