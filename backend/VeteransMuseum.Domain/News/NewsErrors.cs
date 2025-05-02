using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Domain.News;

public static class NewsErrors
{
    public static Error NotFound = new(
        "News.Found",
        "The news with the specified identifier was not found");
    
}