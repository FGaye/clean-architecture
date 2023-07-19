using Microsoft.AspNetCore.Mvc;
using Movie.Api.Controllers;
using Movie.Application.Common.Interface;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Application.Movies.Commands.DeleteMovie;
using Movie.Application.Movies.Commands.EditMovie;
using Movie.Application.Movies.Queries;
using Movie.Application.Movies.Queries.GetMovieById;
using Movie.Application.Movies.Queries.GetMovies;
using Movie.Domain.Entities;
using Movie.Infrastructure.Persistence;
namespace Movie.Api;

public class MovieApiController : BaseApiController
{
    private readonly MovieContext _movieContext;
    private readonly IApplicationDbContext _context;
    public MovieApiController(MovieContext movieContext, IApplicationDbContext context)
    {
        _context = context;
        _movieContext = movieContext;
    }
 
    [HttpPost("create-movie")]
    public async Task<ActionResult> CreateMovie(MovieItem movie)
    {
        return Ok(await Mediator.Send(new CreateMovieCommand.Command { MovieItem = movie }));
    }
 
    [HttpGet("get-all-movies")]
    public async Task<ActionResult<IEnumerable<MovieItem>>> GetMovieItems()
    {
        return Ok(await Mediator.Send(new GetAllMoviesQuery.Query()));
      
    }
   
    [HttpGet("search-movie/{search}")]
    public async Task<ActionResult<List<MovieItem>>> SearchMovies(string search)
    {
      return Ok(await Mediator.Send(new SearchMoviesQuery.Query{Title = search}));
    }
 
    [HttpGet("edit/{id}")]
    public async Task<ActionResult<MovieItem>> GetMovieItem(int id)
    {
        return Ok(await Mediator.Send(new GetMovieByIdQuery.Query { Id = id }));
    }
 
    [HttpPut("update/{id}")]
    public async Task<ActionResult<MovieItem>> EditMovie( MovieItem movies)
    {
      
        return Ok(await Mediator.Send(new  EditMovie.Command{MovieItem = movies} ));
    }
   
    [HttpDelete("delete-movie/{id}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        return Ok(await Mediator.Send(new DeleteMovieCommand.Command { Id = id }));

    }
}