using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.Veterans.UpdateVeteran;

public record UpdateVeteranCommand(
    Guid Id,
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
    string ImageUrl) : ICommand;