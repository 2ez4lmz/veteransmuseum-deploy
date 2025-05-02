using Dapper;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Application.Veterans.GetVeterans;

public class GetVeteransQueryHandler : IQueryHandler<GetVeteransQuery, IReadOnlyList<VeteranResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetVeteransQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VeteranResponse>>> Handle(GetVeteransQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
           SELECT
               id AS Id,
               first_name AS FirstName,
               last_name AS LastName,
               middle_name AS MiddleName,
               birth_date AS BirthDate,
               death_date AS DeathDate,
               biography AS Biography,
               rank AS Rank,
               awards AS Awards,
               military_unit AS MilitaryUnit,
               battles AS Battles,
               image_url AS ImageUrl,
               created_at AS CreatedAt,
               created_by AS CreatedBy,
               updated_at AS UpdatedAt,
               updated_by AS UpdatedBy
           FROM veterans
           """;

        var veterans = await connection.QueryAsync<VeteranResponse>(sql);

        return veterans.ToList();
    }
}