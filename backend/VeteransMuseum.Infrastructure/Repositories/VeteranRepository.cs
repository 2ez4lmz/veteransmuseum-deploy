using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Infrastructure.Repositories;

internal sealed class VeteranRepository: Repository<Veteran>, IVeteranRepository
{
    public VeteranRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}