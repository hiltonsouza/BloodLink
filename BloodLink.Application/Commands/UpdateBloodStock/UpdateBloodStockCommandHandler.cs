using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.UpdateBloodStock
{
    public class UpdateBloodStockCommandHandler : IRequestHandler<UpdateBloodStockCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBloodStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBloodStockCommand request, CancellationToken cancellationToken)
        {
            var bloodStock = await _unitOfWork.BloodStocks.GetByIdAsync(request.Id);

            bloodStock.Update(
                request.BloodType, request.FactorRh, request.BloodVolumeInML
                );

            await _unitOfWork.BloodStocks.UpdateAsync(bloodStock);

            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
