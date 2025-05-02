using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Application.News.GetNews;

namespace VeteransMuseum.Application.News.GetNewsById;

public sealed record GetNewsByIdQuery(Guid NewsId) : IQuery<NewsResponse>;