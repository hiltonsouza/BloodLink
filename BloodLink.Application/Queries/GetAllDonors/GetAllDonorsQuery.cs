using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using MediatR;

namespace BloodLink.Application.Queries.GetAllDonors
{
    public class GetAllDonorsQuery : IRequest<PaginationResult<DonorViewModel>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Page { get; set; } = 1;

        public GetAllDonorsQuery() { }

        public GetAllDonorsQuery(string searchTerm, int page)
        {
            SearchTerm = searchTerm;
            Page = page;
        }
    }
}
