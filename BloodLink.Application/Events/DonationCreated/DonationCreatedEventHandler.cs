using BloodLink.Core.Configurations;
using BloodLink.Core.Entities;
using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Events.DonationCreated
{
    public class DonationCreatedEventHandler : INotificationHandler<DonationCreatedNotification>
    {
        private readonly IBloodStockRepository _bloodStockRepository;
        private readonly INotificationService _notificationService;

        public DonationCreatedEventHandler(IBloodStockRepository bloodStockRepository, INotificationService notificationService)
        {
            _bloodStockRepository = bloodStockRepository;
            _notificationService = notificationService;
        }

        public async Task Handle(DonationCreatedNotification notification, CancellationToken cancellationToken)
        {
            var bloodStock = await _bloodStockRepository.GetDetailsAsync(notification.BloodType, notification.RhFactor);

            if (bloodStock != null)
            {
                bloodStock.IncreaseBloodVolume(notification.BloodVolumeInML);
                await _bloodStockRepository.UpdateAsync(bloodStock);

                if (bloodStock.BloodVolumeInML <= BloodStockThresholds.MinVolumeInML)
                {
                    await _notificationService.NotifyLowStockAsync(
                        bloodStock.BloodType,
                        bloodStock.FactorRh,
                        bloodStock.BloodVolumeInML
                    );
                }
            }
            else
            {
                bloodStock = new BloodStock(
                    notification.BloodType,
                    notification.RhFactor,
                    notification.BloodVolumeInML);

                await _bloodStockRepository.AddAsync(bloodStock);
            }
        }
    }
}
