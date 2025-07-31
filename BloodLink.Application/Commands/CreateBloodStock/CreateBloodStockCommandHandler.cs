using BloodLink.Core.Entities;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.CreateBloodStock
{
    public class CreateBloodStockCommandHandler : IRequestHandler<CreateBloodStockCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBloodStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateBloodStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new BloodStock(request.BloodType, request.FactorRh, request.BloodVolumeInML);

            //unitofwork with transaction
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.BloodStocks.AddAsync(stock);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return stock.Id;
        }
    }
}
