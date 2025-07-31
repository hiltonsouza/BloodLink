using BloodLink.Application.ViewModels;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetRecentDonations
{
    public class GetRecentDonationsQueryHandler : IRequestHandler<GetRecentDonationsQuery, List<RecentDonationViewModel>>
    {
        private readonly IDonationRepository _donationRepository;

        public GetRecentDonationsQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<List<RecentDonationViewModel>> Handle(GetRecentDonationsQuery request, CancellationToken cancellationToken)
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var donations = await _donationRepository.GetAllAsync(0, 1); // Ajuste o método para buscar todas

            var recentDonations = donations.Data
                .Where(d => d.DonationDate >= thirtyDaysAgo)
                .Select(d => new RecentDonationViewModel
                {
                    DonationId = d.Id,
                    DonationDate = d.DonationDate,
                    BloodVolumeInML = d.BloodVolumeInML,
                    DonorName = d.Donor.FullName,
                    DonorEmail = d.Donor.Email,
                    DonorBloodType = d.Donor.BloodType.ToString(),
                    DonorRhFactor = d.Donor.RhFactor
                })
                .ToList();

            return recentDonations;
        }
    }
}