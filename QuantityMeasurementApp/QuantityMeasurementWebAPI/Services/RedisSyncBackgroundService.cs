using Microsoft.Extensions.Hosting;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repository;
namespace QuantityMeasurementWebAPI.Services;
public class RedisSyncBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RedisSyncBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IQuantityMeasurementService>();

                service.SyncQueueToDatabase();
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}