namespace BloodLink.Core.Services
{
    public interface INotificationService
    {
        Task NotifyLowStockAsync(string bloodType, string rhFactor, int bloodVolumeInML);
    }
}
