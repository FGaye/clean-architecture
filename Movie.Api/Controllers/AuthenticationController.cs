
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Authentication.Services;
using Movie.Api.Controllers;
using Movie.Api.Dtos;
using Movie.Infrastructure.Persistence;

namespace Movie.Api;


public class AuthenticationController : BaseApiController
{
    private readonly MovieContext _dbContext;

    public AuthenticationController(MovieContext dbContext)
    {
        _dbContext = dbContext;

    }
    [HttpPost("register")]
    public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerDto)
    {
        // check if username does not exist
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if (user != null)
        {
            return BadRequest("User already exist");
        }

        var users = CreateUser.addUser(registerDto);

        _dbContext.Add(users);

        await _dbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginDto>> Login(LoginDto loginDto)
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == loginDto.Username);

        if (user == null)
        {
            return NotFound();
        }
        var loginResponseDto = new LoginResponseDto()
        {
            Message = "Login successful"
        };
        return Ok(loginResponseDto);
    }


}