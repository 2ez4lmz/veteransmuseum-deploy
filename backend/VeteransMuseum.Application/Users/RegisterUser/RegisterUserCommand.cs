using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;