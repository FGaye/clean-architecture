using FluentValidation;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Authentication
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
            RuleFor(x => x.Password).Length(8, 20).WithMessage("Password must be between 8 and 20 characters");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}