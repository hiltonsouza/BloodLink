using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetAllDonors
{
    public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, PaginationResult<DonorViewModel>>
    {
        private readonly IDonorRepository _donorRepository;

        public GetAllDonorsQueryHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<PaginationResult<DonorViewModel>> Handle(GetAllDonorsQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.GetAllAsync(request.Query, request.Page);

            var donorViewModel = donor
                .Data
                .Select(d => new DonorViewModel(d.Id, d.FullName, d.BirthDate, d.Gender.ToString(), d.Weight, d.BloodType.ToString(), d.RhFactor))
                .ToList();

            var paginationdonorViewModel = new PaginationResult<DonorViewModel>(
                donor.Page,
                donor.TotalPages,
                donor.PageSize,
                donor.ItemsCount,
                donorViewModel);

            return paginationdonorViewModel;
        }
    }
}