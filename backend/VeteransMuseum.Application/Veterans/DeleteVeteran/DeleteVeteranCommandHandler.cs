using VeteransMuseum.Application.Abstractions.Messaging;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.Shared;
using VeteransMuseum.Domain.Users;
using VeteransMuseum.Domain.Veterans;

namespace VeteransMuseum.Application.Veterans.DeleteVeteran;

public class DeleteVeteranCommandHandler : ICommandHandler<DeleteVeteranCommand>
{
    private readonly IVeteranRepository _veteranRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVeteranCommandHandler(
        IVeteranRepository veteranRepository,
        IUnitOfWork unitOfWork)
    {
        _veteranRepository = veteranRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteVeteranCommand request,
        CancellationToken cancellationToken)
    {
        var veteran = await _veteranRepository.GetByIdAsync(request.Id, cancellationToken);

        if (veteran == null)
        {
            return Result.Failure(VeteranErrors.NotFound);
        }

        await _veteranRepository.DeleteAsync(request.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}