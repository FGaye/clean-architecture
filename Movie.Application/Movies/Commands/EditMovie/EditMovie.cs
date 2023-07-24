using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Application.Movies.Commands.CreateMovie;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Commands.EditMovie
{
    public class EditMovie
    {
        public class Command : IRequest<Unit>
        {
          public int Id { get; set; }
          public MovieItemDto Movie { get; set; }
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
        
              var movieItem = await _context.Movies.FindAsync(request.Id);

            if (movieItem == null)
            {
                throw new Exception("Could not find movie");
            }
      
            _mapper.Map(request.Movie, movieItem);
            _context.Movies.Update(movieItem);
            var updatedMovieDto = _mapper.Map<MovieItemDto>(movieItem);
            await  _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
            }
          
            
           

          

           
        }
    }
}