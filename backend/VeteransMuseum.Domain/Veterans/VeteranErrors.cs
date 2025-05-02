using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Domain.Veterans;

public static class VeteranErrors
{
    public static Error NotFound = new(
        "Veteran.Found",
        "The veteran with the specified identifier was not found");
    
}