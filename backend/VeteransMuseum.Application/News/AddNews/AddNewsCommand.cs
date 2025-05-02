using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.News.AddNews;

public record AddNewsCommand(
    string Title,
    string Content,
    string? ImageUrl) : ICommand<Guid>;