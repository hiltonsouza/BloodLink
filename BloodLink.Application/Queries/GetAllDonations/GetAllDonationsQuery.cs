using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using MediatR;

namespace BloodLink.Application.Queries.GetAllDonation
{
    public class GetAllDonationsQuery : IRequest<PaginationResult<DonationViewModel>>
    {
        public int Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
