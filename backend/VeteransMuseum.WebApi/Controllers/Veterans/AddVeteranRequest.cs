namespace VeteransMuseum.WebApi.Controllers.Veterans;

public record AddVeteranRequest(
    string FirstName, 
    string LastName, 
    string MiddleName,
    DateTime? BirthDate,
    DateTime? DeathDate,
    string Biography,
    string Rank,
    string Awards,
    string MilitaryUnit,
    string Battles,
    string ImageUrl);