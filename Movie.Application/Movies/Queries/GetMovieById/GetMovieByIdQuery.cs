using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQuery
    {
        public class Query : IRequest<MovieItem>
        {
            public int Id { get; set; }
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
                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id);

                return movie;
            }
        }
    }
}