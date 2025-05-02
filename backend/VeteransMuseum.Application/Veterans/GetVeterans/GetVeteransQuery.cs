using VeteransMuseum.Application.Abstractions.Messaging;

namespace VeteransMuseum.Application.Veterans.GetVeterans;

public sealed record GetVeteransQuery() : IQuery<IReadOnlyList<VeteranResponse>>;