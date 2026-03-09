using QuantityMeasurementApp.DTO;
using QuantityMeasurementApp.Model;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Entity;

namespace QuantityMeasurementApp.Service;

public class QuantityMeasurementServiceImpl
        : IQuantityMeasurementService
{
    private readonly IQuantityMeasurementRepository repository;

    public QuantityMeasurementServiceImpl(
        IQuantityMeasurementRepository repository)
    {
        this.repository = repository;
    }

    public bool Compare(QuantityDTO q1, QuantityDTO q2)
    {
        var l1 = new Quantity<LengthUnit>(q1.Value,
            Enum.Parse<LengthUnit>(q1.Unit));

        var l2 = new Quantity<LengthUnit>(q2.Value,
            Enum.Parse<LengthUnit>(q2.Unit));

        return l1.Equals(l2);
    }

    public QuantityDTO Convert(QuantityDTO input, string targetUnit)
    {
        var quantity = new Quantity<LengthUnit>(
            input.Value,
            Enum.Parse<LengthUnit>(input.Unit));

        var result = quantity.ConvertTo(
            Enum.Parse<LengthUnit>(targetUnit));

        return new QuantityDTO(
            result.Value,
            targetUnit,
            "Length");
    }

    public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
    {
        var l1 = new Quantity<LengthUnit>(
            q1.Value,
            Enum.Parse<LengthUnit>(q1.Unit));

        var l2 = new Quantity<LengthUnit>(
            q2.Value,
            Enum.Parse<LengthUnit>(q2.Unit));

        var result = l1.Add(l2, LengthUnit.FEET);

        return new QuantityDTO(
            result.Value,
            "FEET",
            "Length");
    }

    public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
    {
        var l1 = new Quantity<LengthUnit>(
            q1.Value,
            Enum.Parse<LengthUnit>(q1.Unit));

        var l2 = new Quantity<LengthUnit>(
            q2.Value,
            Enum.Parse<LengthUnit>(q2.Unit));

        var result = l1.Subtract(l2);

        return new QuantityDTO(
            result.Value,
            q1.Unit,
            "Length");
    }

    public double Divide(QuantityDTO q1, QuantityDTO q2)
    {
        var l1 = new Quantity<LengthUnit>(
            q1.Value,
            Enum.Parse<LengthUnit>(q1.Unit));

        var l2 = new Quantity<LengthUnit>(
            q2.Value,
            Enum.Parse<LengthUnit>(q2.Unit));

        return l1.Divide(l2);
    }
}