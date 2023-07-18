
using Movie.Domain.Entities;

namespace Movie.Application.Authentication.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(Users users);
    }
}