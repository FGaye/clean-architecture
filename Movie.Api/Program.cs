using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application;
using Movie.Application.Movies.Queries.GetMovies;
using Movie.Infrastructure.Persistence;



var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();   
    builder.Services.AddMediatR(typeof(Program).Assembly);

    // builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ApplicationServiceEntryPoint).Assembly));
    // builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


    var configuration = builder.Configuration;
    builder.Services.AddDbContext<MovieContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MovieApiDBConnection")));

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