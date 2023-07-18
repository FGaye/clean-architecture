using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Controllers;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Application.Movies.Queries.GetMovieById;
using Movie.Application.Movies.Queries.GetMovies;
using Movie.Domain.Entities;
using Movie.Infrastructure.Persistence;
namespace Movie.Api;

public class MovieApiController : BaseApiController
{
    private readonly MovieContext _movieContext;
    public MovieApiController(MovieContext movieContext)
    {
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
        return Ok(await Mediator.Send(new GetMoviesQuery.Query()));
    }
    //search for movie  
    [HttpGet]
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
    [HttpGet("{id}/edit")]
    public async Task<ActionResult<MovieItem>> GetMovieItem(int id)
    {
        return Ok(await Mediator.Send(new GetMovieByIdQuery.Query { Id = id }));
    }
    // update the movie
    [HttpPut("{id}/update")]
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
    [HttpDelete("delete")]
    public async Task<ActionResult<MovieItem>> DeleteMovie(int Id)
    {
        var movieItem = await _movieContext.Movies.FindAsync(Id);
        if (movieItem == null)
        {
            return NotFound("Movie  with this Id does not exist");
        }
        _movieContext.Movies.Remove(movieItem);
        await _movieContext.SaveChangesAsync();
        return Ok("Movie successfully deleted");

    }
}