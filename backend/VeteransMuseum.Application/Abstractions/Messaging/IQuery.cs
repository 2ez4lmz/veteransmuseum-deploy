using MediatR;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}