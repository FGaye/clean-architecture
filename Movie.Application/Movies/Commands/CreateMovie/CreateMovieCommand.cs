using AutoMapper;
using FluentValidation;
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
            private readonly IValidator<MovieItemDto> _validator;
            public Handler(IApplicationDbContext context, IMapper mapper, IValidator<MovieItemDto> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // validate movie
                var validationResult = await _validator.ValidateAsync(request.movieItemDto);
                if (!validationResult.IsValid)
                    foreach (var error in validationResult.Errors)
                    {
                        throw new Exception(error.ErrorMessage);
                    }
                var movie = _mapper.Map<MovieItem>(request.movieItemDto);

                _context.Movies.Add(movie);

                var result = await _context.SaveChangesAsync(cancellationToken);


                return Unit.Value;
            }
        }
    }
}