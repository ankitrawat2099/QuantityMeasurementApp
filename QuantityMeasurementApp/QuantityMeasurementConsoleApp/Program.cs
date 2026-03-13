using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementConsoleApp.Interfaces;
using QuantityMeasurementConsoleApp.Services;

class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        bool useDatabase = config.GetValue<bool>("RepositorySettings:UseDatabase");

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(config);

        if (useDatabase)
        {
            services.AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementDatabaseRepository>();
        }
        else
        {
            services.AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>();
        }

        services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

        var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IQuantityMeasurementService>();
        var repository = provider.GetRequiredService<IQuantityMeasurementRepository>();

        // Pass both service and repository to Menu
        IMenu menu = new Menu(service, repository);
        menu.Start();
    }
}