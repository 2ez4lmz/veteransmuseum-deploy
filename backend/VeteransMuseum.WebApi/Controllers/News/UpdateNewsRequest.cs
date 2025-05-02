namespace VeteransMuseum.WebApi.Controllers.News;

public record UpdateNewsRequest(
    string? Title,
    string? Content,
    string? ImageUrl);