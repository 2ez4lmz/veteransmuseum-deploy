using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Application.Veterans.GetVeterans;

namespace VeteransMuseum.Application.Veterans.GetVeteran;

public sealed record GetVeteranQuery(Guid VeteranId) : IQuery<VeteranResponse>;