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
            CreateMap<Board, BoardDto>().ForMember("OrganizationName", opt => opt.MapFrom(c => c.Organization.Name)).ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserTask, TaskDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            CreateMap<TaskFile, TaskFileDto>().ReverseMap();
            CreateMap<AllowedFileTypes, AllowedFileTypeDto>().ReverseMap();
            CreateMap<TaskChangesLog, object>().ReverseMap();
        }
    }
}
