using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Queries
{
    public class SearchMoviesQuery 
    {


        public class Query : IRequest<MovieItem>
        {
           public string Title { get; set; }
        }

        public class Handler : IRequestHandler<Query, MovieItem>
        {
          private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }  

            public async Task<MovieItem> Handle(Query request, CancellationToken cancellationToken)
            {
             var movieItem = await _context.Movies.FirstOrDefaultAsync(x => x.Title.ToLower().Contains(request.Title.ToLower()));
             return movieItem;
            }
        }
    }
}