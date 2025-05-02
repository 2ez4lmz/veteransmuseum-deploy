using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.Users.LogInUser;


public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;