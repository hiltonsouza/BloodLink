using BloodLink.Application.Queries.GetDonationById;
using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetDonationById
{
    /// <summary>
    /// Handles the query to get donations by donor ID.
    /// </summary>
    public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, DonationViewModel>
    {
        private readonly IDonationRepository _donationRepository;

        public GetDonationByIdQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<DonationViewModel> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
        {
            var donation = await _donationRepository.GetByIdAsync(request.Id);
            if(donation is null) return null;

            var donationViewModel = new DonationViewModel(
                donation.Id,
                donation.DonorId,
                donation.Donor != null ? donation.Donor.FullName : string.Empty,
                donation.Donor != null ? donation.Donor.BloodType.ToString() : string.Empty,
                donation.Donor != null ? donation.Donor.RhFactor : string.Empty,
                donation.DonationDate,
                donation.BloodVolumeInML
            );

            return donationViewModel;
        }
    }
}
