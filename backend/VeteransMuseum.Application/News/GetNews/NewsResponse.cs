namespace VeteransMuseum.Application.News.GetNews;

public sealed class NewsResponse
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }
    
    public string Content { get; init; }
    
    public string ImageUrl { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public long CreatedBy { get; init; }
    
    public DateTime? UpdatedAt { get; init; }
    
    public long? UpdatedBy { get; init; }
}