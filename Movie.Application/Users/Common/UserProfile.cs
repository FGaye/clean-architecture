using AutoMapper;
using Movie.Application.Common.Dtos;
using Movie.Application.Dtos;
using Movie.Application.Users.Command;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Common
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // use automapper

            CreateMap<LoginDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, LoginDto>();
            CreateMap<User, RegisterDto>();
          
        }
    }
}