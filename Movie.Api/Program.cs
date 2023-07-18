using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application;
using Movie.Application.Common.Interface;
using Movie.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ApplicationServiceEntryPoint).Assembly));


    var configuration = builder.Configuration;
    builder.Services.AddDbContext<MovieContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("MovieApiDBConnection")));

    // add scoped
    builder.Services.AddScoped<IApplicationDbContext, MovieContext>();

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