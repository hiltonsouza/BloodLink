using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Queries.GetDonorById
{
    public class GetDonorByIdQuery : IRequest<DonorViewModel>
    {
        public GetDonorByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
