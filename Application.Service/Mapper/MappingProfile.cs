using Application.Model;
using Application.ViewModel.Task;
using Application.ViewModel.Value;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserTask, TaskForListDto>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
            CreateMap<ValueForCreateDto, Value>();
            CreateMap<ValueForUpdateDto, Value>();
            CreateMap<ValueForDeleteDto, Value>();            
            CreateMap<Value, ValueForListDto>().ReverseMap();
        }
    }
}
