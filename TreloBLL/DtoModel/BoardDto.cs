using System;
using System.Collections.Generic;
using System.Text;

namespace TreloBLL.DtoModel
{
    public class BoardDto
    {
        public string Name { get; set; }

        public string OrganizationName { get; set; }

        public int OrganizationId { get; set; }

        public List<TaskDto> UserTasks { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
