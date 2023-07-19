using AutoMapper;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Common
{
    public class MovieProfile : Profile
    {
       public MovieProfile()
       {
           CreateMap<MovieItem, MovieItemDto>();
           CreateMap<MovieItemDto, MovieItem>();
       }   
    }
}