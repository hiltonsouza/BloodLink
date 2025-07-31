using BloodLink.Application.ViewModels;
using BloodLink.Core.Resources;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.UpdateDonor
{
    /// <summary>
    /// Handler for updating donor information.
    /// </summary>
    public class UpdateDonorCommandHandler : IRequestHandler<UpdateDonorCommand, ResultViewModel<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDonorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel<Unit>> Handle(UpdateDonorCommand request, CancellationToken cancellationToken)
        {
            var donor = await _unitOfWork.Donors.GetByIdAsync(request.Id);

            if (donor is null)
                return ResultViewModel<Unit>.Error(Messages.DonorNotFound);

            donor.Update(
                request.FullName,
                request.Email,
                request.BirthDate,
                request.Gender,
                request.Weight,
                request.BloodType,
                request.RhFactor,
                request.Address
            );

            //unitofwork with transaction
            await _unitOfWork.Donors.UpdateAsync(donor);
            await _unitOfWork.CompleteAsync();

            return ResultViewModel<Unit>.Success(Unit.Value);
        }
    }
}
