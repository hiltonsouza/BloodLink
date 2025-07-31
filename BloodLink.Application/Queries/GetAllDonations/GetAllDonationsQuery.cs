using BloodLink.Application.Queries.GetDonationById;
using BloodLink.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.Queries.GetAllDonation
{
    public class GetAllDonationsQuery : IRequest<PaginationResult<DonationViewModel>>
    {
        public int Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
