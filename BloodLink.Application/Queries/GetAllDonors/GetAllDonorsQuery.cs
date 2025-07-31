using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using MediatR;

namespace BloodLink.Application.Queries.GetAllDonors
{
    public class GetAllDonorsQuery : IRequest<PaginationResult<DonorViewModel>>
    {
        public string Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
