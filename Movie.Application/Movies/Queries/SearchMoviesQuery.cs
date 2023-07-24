using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Queries
{
    // implement command and handler for search movie by title

    public class SearchMoviesQuery
    {
        public class Query : IRequest<List<MovieItem>>
        {
            public string Title { get; set; }
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
                // get movie by title

                List<MovieItem> movieItems;
                if (string.IsNullOrEmpty(request.Title))
                {
                    movieItems = await _context.Movies.ToListAsync();
                }
                else
                {
                    movieItems = await _context.Movies.Where(x => x.Title.ToLower().Contains(request.Title.ToLower())).ToListAsync();
                }
                return movieItems;
            }
        }
    }
}