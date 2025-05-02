namespace VeteransMuseum.Domain.Veterans;

public interface IVeteranRepository
{
    // Task<IEnumerable<Veteran>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Veteran?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(Veteran news, CancellationToken cancellationToken = default);

    void Add(Veteran news);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}