using VeteransMuseum.Domain.News;

namespace VeteransMuseum.Infrastructure.Repositories;

internal sealed class NewsRepository: Repository<News>, INewsRepository
{
    public NewsRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}