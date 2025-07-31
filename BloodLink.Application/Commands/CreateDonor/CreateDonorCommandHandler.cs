using BloodLink.Application.ViewModels;
using BloodLink.Core.Entities;
using BloodLink.Core.Exceptions;
using BloodLink.Core.Resources;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.CreateDonor
{
    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, ResultViewModel<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViaCepService _viaCepService;
        public CreateDonorCommandHandler(IUnitOfWork unitOfWork, IViaCepService viaCepService)
        {
            _unitOfWork = unitOfWork;
            _viaCepService = viaCepService;
        }

        public async Task<ResultViewModel<int>> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {
            var existingDonor = await _unitOfWork.Donors.GetByEmailAsync(request.Email);

            if (existingDonor)
                return ResultViewModel<int>.Error(Messages.EmailAlreadyExists);

            // Fetch address details using ViaCepService
            var addressResult = await _viaCepService.GetAddressByZipCodeAsync(request.ZipCode, cancellationToken);
            
            if (!addressResult.Success)
                return ResultViewModel<int>.Error(addressResult.Message);

            var address = addressResult.Value;

            var donor = new Donor(request.FullName,
            request.Email,
            request.BirthDate,
            request.Gender,
            request.Weight,
            request.BloodType.ToString(),
            request.RhFactor,
            address
            );

            try
            {
                donor.EnsureEligibility();

                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Donors.AddAsync(donor);

                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (DomainException ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new DomainException(string.Format(Messages.DonorTransactionFailed, ex.Message));
            }

            return ResultViewModel<int>.Success(donor.Id);
        }
    }
}
