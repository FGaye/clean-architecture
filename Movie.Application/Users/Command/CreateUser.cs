using Movie.Application.Common.Dtos;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Command
{
    public class CreateUser
    {
        public static User addUser(RegisterDto registerDto) =>
        
            new User

            {

                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = PasswordHasher.HashPassword(registerDto),
             
             
            };

    }
}