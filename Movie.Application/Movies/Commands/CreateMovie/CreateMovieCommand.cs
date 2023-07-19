using AutoMapper;
using MediatR;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public class Command : IRequest<Unit>
        {
            public MovieItemDto movieItemDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
              var movie = _mapper.Map<MovieItem>(request.movieItemDto);
                
                _context.Movies.Add(movie);
            
                var result = await _context.SaveChangesAsync(cancellationToken);

                if (result == 0)
                    throw new Exception("Could not create movie");

                return Unit.Value;
            }
        }
    }
}