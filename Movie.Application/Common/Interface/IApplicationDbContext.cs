using Microsoft.EntityFrameworkCore;
using Movie.Domain.Entities;

namespace Movie.Application.Common.Interface
{
    public interface IApplicationDbContext
    {
        DbSet<MovieItem> Movies { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}