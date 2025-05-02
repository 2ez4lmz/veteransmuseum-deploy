using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeteransMuseum.Application.Users.GetLoggedInUser;
using VeteransMuseum.Application.Users.LogInUser;
using VeteransMuseum.Application.Users.RegisterUser;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.WebApi.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
 
    public UsersController(ISender sender)
    {
        _sender = sender;
    }
 
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);
 
        var result = await _sender.Send(command, cancellationToken);
 
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
             
        return Ok(result.Value);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LogInUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LogInUserCommand(request.Email, request.Password);
 
        var result = await _sender.Send(command, cancellationToken);
 
        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
 
        return Ok(result.Value);
    }
    
    [HttpGet("me")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();
 
        Result<UserResponse> result = await _sender.Send(query, cancellationToken);
 
        return Ok(result.Value);
    }
}