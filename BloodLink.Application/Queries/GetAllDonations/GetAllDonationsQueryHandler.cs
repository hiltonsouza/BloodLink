using BloodLink.Application.Queries.GetAllDonation;
using BloodLink.Application.Queries.GetDonationById;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.Queries.GetAllDonations
{
    public class GetAllDonationsQueryHandler : IRequestHandler<GetAllDonationsQuery, PaginationResult<DonationViewModel>>
    {
        private readonly IDonationRepository _donationRepository;

        public GetAllDonationsQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<PaginationResult<DonationViewModel>> Handle(GetAllDonationsQuery request, CancellationToken cancellationToken)
        {
            var donations = await _donationRepository.GetAllAsync(request.Query, request.Page);

            var donationsViewModel = donations
                .Data
                .Select(d => new DonationViewModel(d.DonorId, d.DonationDate, d.BloodVolumeInML))
                .ToList();

            var paginationDonationsViewModel = new PaginationResult<DonationViewModel>(
                donations.Page,
                donations.TotalPages,
                donations.PageSize,
                donations.ItemsCount,
                donationsViewModel
                );

            return paginationDonationsViewModel;
        }
    }
}
