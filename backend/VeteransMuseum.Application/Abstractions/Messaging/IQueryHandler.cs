using MediatR;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>, IRequest<Result<TResponse>>
{
}