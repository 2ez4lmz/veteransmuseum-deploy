using VeteransMuseum.Application.Abstractions.Clock;
using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Shared;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Application.Veterans.AddVeteran;

public class AddVeteranCommandHandler : ICommandHandler<AddVeteranCommand, Guid>
{
    private readonly IVeteranRepository _veteranRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
 
    public AddVeteranCommandHandler(
        IUnitOfWork unitOfWork, 
        IVeteranRepository veteranRepository, IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _veteranRepository = veteranRepository;
        _dateTimeProvider = dateTimeProvider;
    }
 
    public async Task<Result<Guid>> Handle(
        AddVeteranCommand request, 
        CancellationToken cancellationToken)
    {
        var veteran = Veteran.Add(
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
            _dateTimeProvider.UtcNow);

        _veteranRepository.Add(veteran);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return veteran.Id;
    }
}