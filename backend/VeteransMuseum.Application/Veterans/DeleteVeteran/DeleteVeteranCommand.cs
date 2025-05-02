using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.Veterans.DeleteVeteran;

public record DeleteVeteranCommand(Guid Id) : ICommand;