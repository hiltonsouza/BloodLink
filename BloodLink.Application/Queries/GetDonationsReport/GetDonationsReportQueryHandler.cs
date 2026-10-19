using BloodLink.Application.ViewModels;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetDonationsReport
{
    public class GetDonationsReportQueryHandler : IRequestHandler<GetDonationsReportQuery, List<DonationReportViewModel>>
    {
        private readonly IDonationRepository _donationRepository;

        public GetDonationsReportQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<List<DonationReportViewModel>> Handle(GetDonationsReportQuery request, CancellationToken cancellationToken)
        {
            var donations = await _donationRepository.GetLastDaysDonationsAsync(30);

            var recentDonations = donations
                .Select(d => new DonationReportViewModel(
                    d.Id,
                    d.DonorId,
                    d.Donor.FullName,
                    d.Donor.Email,
                    d.Donor.BloodType.ToString(),
                    d.BloodVolumeInML,
                    d.DonationDate
                    ))
                .ToList();

            return recentDonations;
        }
    }
}
