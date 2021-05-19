using AutoMapper;
using DDD.Domain.Models;
using DDD.Domain.ViewModels.Users;

namespace CoreMvcAutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserCreateViewModel, User>()
              .ForMember(o => o.LoginId, p => p.MapFrom(q => q.LoginId))
              .ForMember(o => o.UserName, p => p.MapFrom(q => q.UserName))
              .ForMember(o => o.Password, p => p.MapFrom(q => q.Password))
              .ForMember(o => o.ConfirmPassword, p => p.MapFrom(q => q.ConfirmPassword))
              .ForMember(o => o.Salt, p => p.MapFrom(q => q.Salt));
        }
    }
}