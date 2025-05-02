using Microsoft.EntityFrameworkCore;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Infrastructure.Repositories;

internal abstract class Repository<T>
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }
    
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}