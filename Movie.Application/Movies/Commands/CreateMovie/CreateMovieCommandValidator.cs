using FluentValidation;

namespace Movie.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<MovieItemDto>
        {
            public CreateMovieCommandValidator()
            {
                
                RuleFor(x => x.Title).Length(2, 50).WithMessage("Title must be between 2 and 50 characters");
                RuleFor(x => x.Genre).Length(2, 10).WithMessage("Genre must be between 2 and 10 characters");
            }
        }
}