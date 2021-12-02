using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TreloBLL.DtoModel;
using TreloBLL.Services;
using TreloDAL.Models;

namespace TreloBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganiztionDto>().ReverseMap();
            CreateMap<Board, BoardDto>().ForMember(p=>p.OrganizationName, opt => opt.MapFrom(c => c.Organization.Name)).ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserTask, TaskDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            CreateMap<TaskFile, TaskFileDto>().ReverseMap();
            CreateMap<AllowedFileTypes, AllowedFileTypeDto>().ReverseMap();
            CreateMap<TaskChangesLog, LogGeneralData>().ForMember(p=>p.EntityId, opt=>opt.MapFrom(c=>c.TaskId)).ReverseMap();
            CreateMap<TaskFileDto, FileGeneralDto>().ForMember(p=>p.DataFile, opt => opt.MapFrom(c => c.DataFiles)).ReverseMap();
            CreateMap<UserCredentialFile, FileGeneralDto>().ForMember(p=>p.DataFile, opt => opt.MapFrom(c => c.FileData)).ReverseMap();
        }
    }
}
