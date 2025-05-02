using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.News.UpdateNews;

public record UpdateNewsCommand(
    Guid Id,
    string? Title,
    string? Content,
    string? ImageUrl) : ICommand;