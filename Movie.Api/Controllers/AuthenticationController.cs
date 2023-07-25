
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Api.Controllers;
using Movie.Application.Common.Dtos;
using Movie.Application.Dtos;
using Movie.Application.Users.Command;
using Movie.Application.Users.Query;
using Movie.Domain.Entities;
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
    public async Task<ActionResult> Register(User createUser)
    {
        // check if username does not exist
       return Ok(await Mediator.Send(new CreateUser.Command { user = createUser }));
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginDto>> Login(User user)
    {
        return Ok(await Mediator.Send(new Login.LoginQuery{Username = user.Name, Password = user.Password }));
    }


}