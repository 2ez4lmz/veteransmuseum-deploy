namespace VeteransMuseum.WebApi.Controllers.News;

public record AddNewsRequest(
    string Title,
    string Content,
    string? ImageUrl);