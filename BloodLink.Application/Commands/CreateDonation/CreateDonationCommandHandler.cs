using BloodLink.Application.ViewModels;
using BloodLink.Core.Configurations;
using BloodLink.Core.Entities;
using BloodLink.Core.Exceptions;
using BloodLink.Core.Repositories;
using BloodLink.Core.Resources;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, ResultViewModel<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        public CreateDonationCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<ResultViewModel<int>> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var donor = await _unitOfWork.Donors.GetByIdAsync(request.DonorId);
                if (donor is null)
                    return ResultViewModel<int>.Error(Messages.DonorNotFound);

                var donation = new Donation(request.DonorId, request.BloodVolumeInML);

                try
                {
                    donor.EnsureEligibility();
                    donor.EnsureDonationInterval();
                    donation.ValidateBloodQuantity();
                }
                catch (DomainException ex)
                {
                    return ResultViewModel<int>.Error(ex.Message);
                }

                // Start a transaction
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Donations.AddAsync(donation);

                var bloodStock = await _unitOfWork.BloodStocks.GetDetailsAsync(donor.BloodType.ToString(), donor.RhFactor);

                if (bloodStock != null)
                {
                    // If blood stock exists, increase the volume
                    bloodStock.IncreaseBloodVolume(donation.BloodVolumeInML);
                    await _unitOfWork.BloodStocks.UpdateAsync(bloodStock);

                    // Check if the blood stock is below the threshold
                    if (bloodStock.BloodVolumeInML <= BloodStockThresholds.MinVolumeInML)
                        await _notificationService.NotifyLowStockAsync(
                            bloodStock.BloodType,
                            bloodStock.FactorRh,
                            bloodStock.BloodVolumeInML
                        );
                }
                else
                {
                    bloodStock = new BloodStock(
                        donor.BloodType.ToString(),
                        donor.RhFactor,
                        donation.BloodVolumeInML);

                    await _unitOfWork.BloodStocks.AddAsync(bloodStock);
                }

                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitAsync();


                return ResultViewModel<int>.Success(donation.Id);
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of error
                await _unitOfWork.RollbackAsync();
                throw new DomainException(string.Format(Messages.DonationTransactionFailed, ex.Message));
            }
        }
    }
}
