using BloodLink.Application.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace BloodLink.Application.Queries.GetRecentDonations
{
    public class GetRecentDonationsQuery : IRequest<List<RecentDonationViewModel>>
    {
    }
}