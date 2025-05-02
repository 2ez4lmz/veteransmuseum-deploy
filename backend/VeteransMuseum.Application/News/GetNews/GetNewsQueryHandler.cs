using Dapper;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Application.News.GetNewsById;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Application.News.GetNews;

public class GetNewsQueryHandler : IQueryHandler<GetNewsQuery, IReadOnlyList<NewsResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNewsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Result<IReadOnlyList<NewsResponse>>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
           SELECT
               id AS Id,
               title AS Title,
               content AS Content,
               image_url AS ImageUrl,
               created_at AS CreatedAt,
               created_by AS CreatedBy,
               updated_at AS UpdatedAt,
               updated_by AS UpdatedBy
           FROM news
           """;

        var news = await connection.QueryAsync<NewsResponse>(sql);

        return news.ToList();
    }
}