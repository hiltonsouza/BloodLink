using BloodLink.Core.Models;
using MediatR;

namespace BloodLink.Application.Queries.GetDonationsByDonorId
{
    public class GetDonationsByDonorIdQuery : IRequest<PaginationResult<DonationViewModel>>
    {
        public GetDonationsByDonorIdQuery(int donorId, int page)
        {
            DonorId = donorId;
            Page = page;
        }

        public int DonorId { get; set; }
        public int Page { get; set; } = 1;
    }
}
