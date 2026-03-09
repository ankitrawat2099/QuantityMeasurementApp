using QuantityMeasurementApp.Controller;
using QuantityMeasurementApp.Service;
using QuantityMeasurementApp.Repository;

namespace QuantityMeasurementApp;

public class QuantityMeasurementApp
{
    public static void Main()
    {
        var repo =
            QuantityMeasurementCacheRepository.GetInstance();

        var service =
            new QuantityMeasurementServiceImpl(repo);

        var controller =
            new QuantityMeasurementController(service);

        controller.PerformAddition();
    }
}