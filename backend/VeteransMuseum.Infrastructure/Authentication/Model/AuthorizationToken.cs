using System.Text.Json.Serialization;

namespace VeteransMuseum.Infrastructure.Authentication.Model;

internal sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}