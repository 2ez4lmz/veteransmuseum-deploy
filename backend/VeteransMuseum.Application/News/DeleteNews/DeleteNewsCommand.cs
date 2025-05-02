using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.News.DeleteNews;

public record DeleteNewsCommand(Guid Id) : ICommand;