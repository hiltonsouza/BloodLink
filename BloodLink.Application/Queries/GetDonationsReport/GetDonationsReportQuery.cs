using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Queries.GetDonationsReport
{
    public class GetDonationsReportQuery : IRequest<List<DonationReportViewModel>>
    {
    }
}
