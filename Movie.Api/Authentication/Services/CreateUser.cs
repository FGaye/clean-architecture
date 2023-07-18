using Movie.Api.Dtos;
using Movie.Domain.Entities;

namespace Movie.Api.Authentication.Services
{
    public class CreateUser
    {
        public static Users addUser(RegisterDto registerDto) =>
        
            new Users

            {

                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = PasswordHasher.HashPassword(registerDto),
             
             
            };

    }
}