using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TreloBLL.DtoModel;
using TreloDAL.Models;

namespace TreloBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganiztionDto>().ReverseMap();
            CreateMap<Board, BoardDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserTask, TaskDto>().ReverseMap();
        }
    }
}
