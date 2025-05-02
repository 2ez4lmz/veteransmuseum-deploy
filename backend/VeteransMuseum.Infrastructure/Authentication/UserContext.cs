using Microsoft.AspNetCore.Http;
using VeteransMuseum.Application.Abstractions.Authentication;

namespace VeteransMuseum.Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
 
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
 
    public Guid UserId { get; }
 
    public string IdentityId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetIdentityId() ??
        throw new ApplicationException("User context is unavailable");
}