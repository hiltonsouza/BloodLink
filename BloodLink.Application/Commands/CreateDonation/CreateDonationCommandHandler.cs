using BloodLink.Application.ViewModels;
using BloodLink.Core.Configurations;
using BloodLink.Core.Entities;
using BloodLink.Core.Events;
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
        private readonly IMediator _mediator;
        public CreateDonationCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _mediator = mediator;
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

                // Publish Domain Event (Execute Logic to Update Stock in same Context)
                await _mediator.Publish(new BloodLink.Application.Events.DonationCreated.DonationCreatedNotification(
                    donation.Id, 
                    donor.BloodType.ToString(), 
                    donor.RhFactor, 
                    donation.BloodVolumeInML), cancellationToken);

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
