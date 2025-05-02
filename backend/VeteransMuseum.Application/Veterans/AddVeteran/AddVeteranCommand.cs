using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Application.Veterans.AddVeteran;

public record AddVeteranCommand(
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
    string ImageUrl) : ICommand<Guid>;