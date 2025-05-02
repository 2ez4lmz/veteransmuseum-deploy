using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.News;
using VeteransMuseum.Domain.Shared;

namespace VeteransMuseum.Application.News.AddNews;

public class AddNewsCommandHandler : ICommandHandler<AddNewsCommand, Guid>
{
    private readonly INewsRepository _newsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddNewsCommandHandler(
        INewsRepository newsRepository,
        IUnitOfWork unitOfWork)
    {
        _newsRepository = newsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        AddNewsCommand request,
        CancellationToken cancellationToken)
    {
        var news = Domain.News.News.Add(
            new Title(request.Title),
            new Content(request.Content),
            request.ImageUrl != null ? new ImageUrl(request.ImageUrl) : null,
            DateTime.UtcNow);

        _newsRepository.Add(news);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return news.Id;
    }
}