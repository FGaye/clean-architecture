using MediatR;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public class Command : IRequest<Unit>
        {
            public MovieItem MovieItem { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var movie = new MovieItem
                {
                    Title = request.MovieItem.Title,
                    Description = request.MovieItem.Description,
                    Price = request.MovieItem.Price,
                    Genre = request.MovieItem.Genre
                };

                _context.Movies.Add(movie);

                var result = await _context.SaveChangesAsync(cancellationToken);

                if (result == 0)
                    throw new Exception("Could not create movie");

                return Unit.Value;
            }
        }
    }
}