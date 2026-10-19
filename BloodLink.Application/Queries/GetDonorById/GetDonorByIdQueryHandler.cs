using BloodLink.Application.ViewModels;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetDonorById
{
    public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, DonorViewModel>
    {
        private readonly IDonorRepository _donorRepository;

        public GetDonorByIdQueryHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<DonorViewModel> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.GetByIdAsync(request.Id);

            if (donor == null) return null;

            return new DonorViewModel(
                donor.Id,
                donor.FullName,
                donor.Email,
                donor.BirthDate,
                donor.Gender.ToString(),
                donor.Weight,
                donor.BloodType.ToString(),
                donor.RhFactor,
                donor.Address != null ? new AddressViewModel(donor.Address.Street, donor.Address.City, donor.Address.State, donor.Address.ZipCode) : null,
                donor.Donations.Select(d => new DonationViewModel(
                    d.Id, 
                    donor.Id, 
                    donor.FullName, 
                    donor.BloodType.ToString(), 
                    donor.RhFactor, 
                    d.DonationDate, 
                    d.BloodVolumeInML)).ToList());
        }
    }
}
