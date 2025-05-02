using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.News;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.Application.News.DeleteNews;

public class DeleteNewsCommandHandler : ICommandHandler<DeleteNewsCommand>
{
    private readonly INewsRepository _newsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNewsCommandHandler(
        INewsRepository newsRepository,
        IUnitOfWork unitOfWork)
    {
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteNewsCommand request,
        CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (news == null)
        {
            return Result.Failure(NewsErrors.NotFound);
        }

        await _newsRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}