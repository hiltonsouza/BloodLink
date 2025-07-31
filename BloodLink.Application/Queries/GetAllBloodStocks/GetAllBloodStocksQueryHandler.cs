using BloodLink.Application.ViewModels;
using BloodLink.Core.Models;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Queries.GetAllBloodStocks
{
    public class GetAllBloodStocksQueryHandler : IRequestHandler<GetAllBloodStocksQuery, PaginationResult<BloodStockReportViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBloodStocksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<BloodStockReportViewModel>> Handle(GetAllBloodStocksQuery request, CancellationToken cancellationToken)
        {         
            var bloodStock = await _unitOfWork.BloodStocks.GetAllAsync(request.Query, request.Page);

            var bloodStockViewModel = bloodStock
                .Data
                .Select(b => new BloodStockReportViewModel(
                    b.BloodType, 
                    b.FactorRh, 
                    b.BloodVolumeInML))
                .ToList();

            var paginationbloodStockViewModel = new PaginationResult<BloodStockReportViewModel>(
                bloodStock.Page,
                bloodStock.TotalPages,
                bloodStock.PageSize,
                bloodStock.ItemsCount,
                bloodStockViewModel);

            return paginationbloodStockViewModel;
        }
    }
}
