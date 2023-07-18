using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Controllers;
using Movie.Application.Common.Interface;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Application.Movies.Commands.DeleteMovie;
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
    // create movie
    [HttpPost("create-movie")]
    public async Task<ActionResult> CreateMovie(MovieItem movie)
    {
        return Ok(await Mediator.Send(new CreateMovieCommand.Command { MovieItem = movie }));
    }
    //get all the movies
    [HttpGet("get-all-movies")]
    public async Task<ActionResult<IEnumerable<MovieItem>>> GetMovieItems()
    {
        return Ok(await Mediator.Send(new GetAllMoviesQuery.Query()));
        // return Ok(await _context.Movies.ToListAsync());
    }
    //search for movie  
    [HttpGet("search-movie/{search}")]
    public async Task<ActionResult<List<MovieItem>>> SearchMovies(string search)
    {
        var movieItem = await _movieContext.Movies.Where(t =>
        t.Title.ToLower()
        .Contains(search.ToLower()) || t.Description.ToLower()
        .Contains(search.ToLower()) || t.Genre.ToLower()
        .Contains(search.ToLower()))
        .ToListAsync();
        return movieItem;
    }
    //get movie by id
    [HttpGet("edit/{id}")]
    public async Task<ActionResult<MovieItem>> GetMovieItem(int id)
    {
        return Ok(await Mediator.Send(new GetMovieByIdQuery.Query { Id = id }));
    }
    // update the movie
    [HttpPut("update/{id}")]
    public async Task<ActionResult<MovieItem>> EditMovie(int Id, MovieItem movies)
    {
        var movieItem = await _movieContext.Movies.FindAsync(Id);

        if (movieItem == null)
        {
            return NotFound("Movie does not exist");
        }
        movieItem.Title = movies.Title;
        movieItem.Description = movies.Description;
        movieItem.Price = movies.Price;
        movieItem.Genre = movies.Genre;

        await _movieContext.SaveChangesAsync();
        return Ok("Saved edited movie");
    }
    //delete movie
    [HttpDelete("delete-movie/{id}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        return Ok(await Mediator.Send(new DeleteMovieCommand.Command { Id = id }));

    }
}