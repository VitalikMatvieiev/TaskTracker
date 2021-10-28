using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.DtoModel
{
    public class AllowedFileTypeDto
    {
        public string FileType { get; set; }
        public double AllowedSize { get; set; }
        public bool IsAllowed { get; set; }
    }
}
