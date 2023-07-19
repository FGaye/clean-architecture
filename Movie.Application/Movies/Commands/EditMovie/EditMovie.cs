using MediatR;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Movies.Commands.EditMovie
{
    public class EditMovie
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
        
            var movieItem = await _context.Movies.FindAsync(request.MovieItem.Id);

            if (movieItem == null)
            {
                throw new Exception("Could not find movie");
            }

            var movie = new MovieItem{
                Title = request.MovieItem.Title,
                Description = request.MovieItem.Description,
                Price = request.MovieItem.Price,
                Genre = request.MovieItem.Genre
            };
            _context.Movies.Update(movie);
           await  _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
            }
          
            
           

          

           
        }
    }
}