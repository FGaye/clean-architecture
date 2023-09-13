using MediatR;
using Movie.Application.Common.Interface;

namespace Movie.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public class Command : IRequest<Unit>
        {
            public int Id { get; set; }
        }


        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies.FindAsync(request.Id) ?? throw new Exception("Could not find movie");
                _context.Movies.Remove(movie);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                    throw new Exception("Problem deleting movie");

                return Unit.Value;
            }
        }
    }
}