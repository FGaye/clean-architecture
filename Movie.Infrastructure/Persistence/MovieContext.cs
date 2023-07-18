using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Infrastructure.Persistence;

public class MovieContext : DbContext, IApplicationDbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    public DbSet<MovieItem> Movies => Set<MovieItem>();

    public DbSet<Users> Users => Set<Users>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}