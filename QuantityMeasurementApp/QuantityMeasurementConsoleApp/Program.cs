using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;

class Program
{
    static void Main()
    {
        var provider = new ServiceCollection()
            .AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>()
            .AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>()
            .BuildServiceProvider();

        var service = provider.GetService<IQuantityMeasurementService>();

        Menu menu = new Menu(service);
        menu.Start();
    }
}