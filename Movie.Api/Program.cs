using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movie.Application;
using Movie.Application.Common.Interface;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Application.Movies.Common;
using Movie.Application.Users.Authentication;
using Movie.Application.Users.Common;
using Movie.Domain.Entities;
using Movie.Infrastructure.Persistence;
using static Movie.Application.Movies.Commands.CreateMovie.CreateMovieCommand;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(MovieProfile).Assembly,typeof(UserProfile).Assembly);

    builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ApplicationServiceEntryPoint).Assembly));


    var configuration = builder.Configuration;
    builder.Services.AddDbContext<MovieContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MovieApiDBConnection")));

    // add scoped
    builder.Services.AddScoped<IApplicationDbContext, MovieContext>();

    // services.AddScoped<IValidator<Person>, PersonValidator>();
    builder.Services.AddScoped<IValidator<MovieItemDto>, CreateMovieCommandValidator>();
    builder.Services.AddScoped<IValidator<User>, UserValidator>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5173");
        });
    });
    var app = builder.Build();
    {

        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}