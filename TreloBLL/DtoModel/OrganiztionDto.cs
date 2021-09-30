using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.DtoModel
{
    public class OrganiztionDto
    {
        public string Name { get; set; }

        public List<BoardDto> Boards { get; set; }
    }
}
