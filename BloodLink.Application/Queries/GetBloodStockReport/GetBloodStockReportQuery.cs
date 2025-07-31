using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Queries.GetBloodStockReport
{
    public class GetBloodStockReportQuery : IRequest<List<BloodStockReportViewModel>>
    {
    }
}