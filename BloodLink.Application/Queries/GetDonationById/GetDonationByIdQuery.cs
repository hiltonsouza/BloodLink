using MediatR;

namespace BloodLink.Application.Queries.GetDonationById
{
    public class GetDonationByIdQuery : IRequest<DonationViewModel>
    {
        public GetDonationByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
