using BloodLink.Core.Services;

namespace BloodLink.Infrastructure.Persistence.Services
{
    /// <summary>
    /// Provides functionality to send notifications about low stock levels of blood types.
    /// </summary>
    /// <remarks>This service can be used to alert relevant parties when the stock of a specific blood type
    /// falls below a certain threshold. Notifications may be sent via email or other configured channels.</remarks>
    public class NotificationService : INotificationService
    {
        public Task NotifyLowStockAsync(string bloodType, string rhFactor, int bloodVolumeInML)
        {
            // Implement the logic to notify about low stock on email or other channels
            // This could involve sending an email, logging, or integrating with a notification service
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [WARNING] Low stock on {bloodType}{rhFactor}: {bloodVolumeInML}ml remaining");
            return Task.CompletedTask;
        }
    }
}
