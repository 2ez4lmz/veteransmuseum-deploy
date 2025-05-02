using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.News;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.Application.News.UpdateNews;

public class UpdateNewsCommandHandler : ICommandHandler<UpdateNewsCommand>
{
    private readonly INewsRepository _newsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNewsCommandHandler(
        INewsRepository newsRepository,
        IUnitOfWork unitOfWork)
    {
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateNewsCommand request,
        CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (news == null)
        {
            return Result.Failure(NewsErrors.NotFound);
        }

        news.Update(
            request.Title != null ? new Title(request.Title) : null,
            request.Content != null ? new Content(request.Content) : null,
            request.ImageUrl != null ? new ImageUrl(request.ImageUrl) : null,
            DateTime.UtcNow,
            0); // Replace with actual user ID if available

        await _newsRepository.UpdateAsync(news, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}