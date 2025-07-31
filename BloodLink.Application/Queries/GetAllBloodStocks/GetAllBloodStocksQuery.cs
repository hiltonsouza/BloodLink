using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.Queries.GetAllBloodStocks
{
    public class GetAllBloodStocksQuery : IRequest<PaginationResult<BloodStockReportViewModel>>
    {
        public string Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
