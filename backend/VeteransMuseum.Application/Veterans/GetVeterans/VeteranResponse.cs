namespace VeteransMuseum.Application.Veterans.GetVeterans;

public sealed class VeteranResponse
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string MiddleName { get; init; }
    
    public DateTime? BirthDate { get; init; }
    
    public DateTime? DeathDate { get; init; }
    
    public string Biography { get; init; }
    
    public string Rank { get; init; }
    
    public string Awards { get; init; }
    
    public string MilitaryUnit { get; init; }
    
    public string Battles { get; init; }
    
    public string ImageUrl { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public long CreatedBy { get; init; }
    
    public DateTime? UpdatedAt { get; init; }
    
    public long? UpdatedBy { get; init; }
}