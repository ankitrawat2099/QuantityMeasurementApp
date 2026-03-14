using Microsoft.Extensions.Configuration;
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
            .AddJsonFile("appsettings.json")
            .Build();

        Console.WriteLine("===== Select Storage =====");
        Console.WriteLine("1 Cache");
        Console.WriteLine("2 Database");
        Console.Write("Enter Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        IQuantityMeasurementRepository repository;

        if (choice == 1)
        {
            repository = new QuantityMeasurementCacheRepository();
        }
        else if (choice == 2)
        {
            repository = new QuantityMeasurementDatabaseRepository(config);
        }
        else
        {
            Console.WriteLine("Invalid Choice");
            return;
        }

        IQuantityMeasurementService service =
            new QuantityMeasurementServiceImpl(repository);

        IMenu menu = new Menu(service, repository);

        menu.Start();
    }
}