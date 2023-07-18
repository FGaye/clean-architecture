using Movie.Infrastructure.Persistence;

namespace Movie.Api.Authentication.Services
{


    public class UserRepository
{
    private readonly MovieContext _dbContext;

    public UserRepository(MovieContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  bool EmailExists(string email)
    {
        bool emailExists = _dbContext.Users.Any(u => u.Email == email);
        return emailExists;
    }
}

}