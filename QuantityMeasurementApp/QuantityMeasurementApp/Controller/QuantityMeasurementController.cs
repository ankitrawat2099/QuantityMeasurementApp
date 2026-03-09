using QuantityMeasurementApp.DTO;
using QuantityMeasurementApp.Service;

namespace QuantityMeasurementApp.Controller;

public class QuantityMeasurementController
{
    private readonly IQuantityMeasurementService service;

    public QuantityMeasurementController(
        IQuantityMeasurementService service)
    {
        this.service = service;
    }

    public void PerformAddition()
    {
        var q1 = new QuantityDTO(1, "FEET", "Length");
        var q2 = new QuantityDTO(12, "INCH", "Length");

        var result = service.Add(q1, q2);

        Console.WriteLine(
            $"Result = {result.Value} {result.Unit}");
    }
}