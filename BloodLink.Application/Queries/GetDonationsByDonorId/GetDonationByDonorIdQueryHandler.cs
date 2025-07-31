using BloodLink.Application.Queries.GetDonationsByDonorId;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetDonationsByDonorId
{
    /// <summary>
    /// Handles the query to get donations by donor ID.
    /// </summary>
    public class GetDonationsByDonorIdQueryHandler : IRequestHandler<GetDonationsByDonorIdQuery, PaginationResult<DonationViewModel>>
    {
        private readonly IDonationRepository _donationRepository;

        public GetDonationsByDonorIdQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<PaginationResult<DonationViewModel>> Handle(GetDonationsByDonorIdQuery request, CancellationToken cancellationToken)
        {
            var donations = await _donationRepository.GetDonationByDonorIdAsync(request.DonorId, request.Page);

            var donationsViewModel = donations.Data
                .Select(d => new DonationViewModel(
                    d.DonorId,
                    d.DonationDate,
                    d.BloodVolumeInML
                ))
                .ToList();

            return new PaginationResult<DonationViewModel>(
                donations.Page,
                donations.TotalPages,
                donations.PageSize,
                donations.ItemsCount,
                donationsViewModel
                );
        }
    }
}
