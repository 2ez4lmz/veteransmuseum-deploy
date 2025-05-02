using Dapper;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Application.News.GetNews;
using VeteransMuseum.Application.Veterans.GetVeterans;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.News;

namespace VeteransMuseum.Application.News.GetNewsById;

public class GetNewsByIdQueryHandler : IQueryHandler<GetNewsByIdQuery, NewsResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNewsByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<NewsResponse>> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
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
           WHERE id = @NewsId
           """;

        var news = await connection.QueryFirstOrDefaultAsync<NewsResponse>(
            sql,
            new
            {
                request.NewsId
            });
        
        if (news == null)
        {
            return Result.Failure<NewsResponse>(NewsErrors.NotFound);
        }

        return news;
    }
}