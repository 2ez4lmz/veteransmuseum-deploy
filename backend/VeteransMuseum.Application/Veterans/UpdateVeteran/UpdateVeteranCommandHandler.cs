using VeteransMuseum.Application.Abstractions.Clock;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Shared;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Application.Veterans.UpdateVeteran;

public class UpdateVeteranCommandHandler : ICommandHandler<UpdateVeteranCommand>
{
    private readonly IVeteranRepository _veteranRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateVeteranCommandHandler(
        IVeteranRepository veteranRepository,
        IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _veteranRepository = veteranRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(
        UpdateVeteranCommand request,
        CancellationToken cancellationToken)
    {
        var veteran = await _veteranRepository.GetByIdAsync(request.Id, cancellationToken);

        if (veteran == null)
        {
            return Result.Failure(VeteranErrors.NotFound);
        }

        veteran.Update(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new MiddleName(request.MiddleName),
            request.BirthDate,
            request.DeathDate,
            new Biography(request.Biography),
            new Rank(request.Rank),
            new Award(request.Awards),
            new MilitaryUnit(request.MilitaryUnit),
            new Battle(request.Battles),
            request.ImageUrl != null ? new ImageUrl(request.ImageUrl) : null,
            _dateTimeProvider.UtcNow,
            0);

        await _veteranRepository.UpdateAsync(veteran, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}