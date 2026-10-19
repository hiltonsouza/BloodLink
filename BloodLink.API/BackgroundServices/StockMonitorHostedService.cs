using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BloodLink.API.BackgroundServices
{
    public class StockMonitorHostedService : BackgroundService
    {
        private readonly ILogger<StockMonitorHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private const int CHECK_INTERVAL_HOURS = 24;

        public StockMonitorHostedService(ILogger<StockMonitorHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stock Monitor Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking blood stock levels...");

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var bloodStockRepository = scope.ServiceProvider.GetRequiredService<IBloodStockRepository>();
                        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                        var allStocks = await bloodStockRepository.GetAllRawAsync();

                        foreach (var stock in allStocks)
                        {
                            if (stock.BloodVolumeInML <= BloodLink.Core.Configurations.BloodStockThresholds.MinVolumeInML)
                            {
                                _logger.LogWarning($"Low Stock Alert! Type: {stock.BloodType}{stock.FactorRh}, Volume: {stock.BloodVolumeInML}ml");
                                
                                await notificationService.NotifyLowStockAsync(stock.BloodType, stock.FactorRh, stock.BloodVolumeInML);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while monitoring stock.");
                }

                await Task.Delay(TimeSpan.FromHours(CHECK_INTERVAL_HOURS), stoppingToken);
            }
        }
    }
}
