using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.News.GetNews;

public sealed record GetNewsQuery() : IQuery<IReadOnlyList<NewsResponse>>;