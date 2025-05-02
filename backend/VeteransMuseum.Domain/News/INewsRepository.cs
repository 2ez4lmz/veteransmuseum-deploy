namespace VeteransMuseum.Domain.News;

public interface INewsRepository
{
    //Task<IEnumerable<News>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<News?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(News news, CancellationToken cancellationToken = default);

    void Add(News news);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}