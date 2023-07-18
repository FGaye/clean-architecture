using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        public class Query : IRequest<List<MovieItem>>
        {

        }

        public class Handler : IRequestHandler<Query, List<MovieItem>>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<MovieItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Movies.ToListAsync();
            }
        }

    }
}