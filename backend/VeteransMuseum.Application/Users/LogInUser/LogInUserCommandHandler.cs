using VeteransMuseum.Application.Abstractions.Authentication;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Users;

namespace VeteransMuseum.Application.Users.LogInUser;

public sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;
 
    public LogInUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
 
 
    public async Task<Result<AccessTokenResponse>> Handle(
        LogInUserCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);
 
        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }
 
        return new AccessTokenResponse(result.Value);
    }
}