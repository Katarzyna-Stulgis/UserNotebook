using AutoMapper;
using UserNotebook.Domain.Models.Entities;
using UserNotebook.Domain.Models.Enums;
using UserNotebook.Service.Dtos;

namespace UserNotebook.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.GetDescription()))
                 .ReverseMap();
        }
    }
}