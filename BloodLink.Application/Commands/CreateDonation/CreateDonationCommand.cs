using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Commands.CreateDonation
{
    public class CreateDonationCommand : IRequest<ResultViewModel<int>>
    {
        public int DonorId { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
