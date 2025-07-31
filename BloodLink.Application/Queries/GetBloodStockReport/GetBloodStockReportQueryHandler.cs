using BloodLink.Application.ViewModels;
using BloodLink.Core.Repositories;
using MediatR;

namespace BloodLink.Application.Queries.GetBloodStockReport
{
    public class GetBloodStockReportQueryHandler : IRequestHandler<GetBloodStockReportQuery, List<BloodStockReportViewModel>>
    {
        private readonly IBloodStockRepository _bloodStockRepository;

        public GetBloodStockReportQueryHandler(IBloodStockRepository bloodStockRepository)
        {
            _bloodStockRepository = bloodStockRepository;
        }

        public async Task<List<BloodStockReportViewModel>> Handle(GetBloodStockReportQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _bloodStockRepository.GetAllAsync("", 1);

            var report = stocks.Data
                .GroupBy(s => new { s.BloodType, s.FactorRh })
                .Select(g => new BloodStockReportViewModel
                (
                    g.Key.BloodType,
                    g.Key.FactorRh,
                    g.Sum(x => x.BloodVolumeInML)
                ))
                .ToList();

            return report;
        }
    }
}