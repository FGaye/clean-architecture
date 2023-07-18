
using Movie.Api.Authentication.Services;
using Movie.Api.Dtos;

public class UserService
{
    private readonly UserRepository _userRepository;
    private RegisterDto registerDto;

    public UserService(RegisterDto registerDto)
    {
        this.registerDto = registerDto;
    }

    public UserService(UserRepository userRepository, RegisterDto registerDto)
    {
        _userRepository = userRepository;
        this.registerDto = registerDto;
    }

   
    public bool IsEmailAvailable(string email)
    {
        bool emailAvailable = !_userRepository.EmailExists(email);
        return emailAvailable;
    }
}
