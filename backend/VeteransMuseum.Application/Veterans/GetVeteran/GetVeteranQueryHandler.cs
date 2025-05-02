using Dapper;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Application.Veterans.GetVeterans;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Application.Veterans.GetVeteran;

public class GetVeteranQueryHandler : IQueryHandler<GetVeteranQuery, VeteranResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetVeteranQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<VeteranResponse>> Handle(GetVeteranQuery request, CancellationToken cancellationToken)
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
           WHERE id = @VeteranId
           """;

        var veteran = await connection.QueryFirstOrDefaultAsync<VeteranResponse>(
            sql,
            new
            {
                request.VeteranId
            });

        if (veteran == null)
        {
            return Result.Failure<VeteranResponse>(VeteranErrors.NotFound);
        }
        
        return veteran;
    }
}