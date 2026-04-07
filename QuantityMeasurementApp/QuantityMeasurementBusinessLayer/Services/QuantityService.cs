using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementBusinessLayer.Services;

public class QuantityService
{
    private const double EPSILON = 0.00001;

    private double ConvertToBaseUnit<U>(QuantityModel<U> quantity)
        where U : struct, Enum
    {
        if (quantity == null)
            throw new ArgumentNullException(nameof(quantity));

        if (quantity.Unit is LengthUnit length)
            return length.ConvertToBaseUnit(quantity.Value);

        if (quantity.Unit is WeightUnit weight)
            return weight.ConvertToBaseUnit(quantity.Value);

        if (quantity.Unit is VolumeUnit volume)
            return volume.ConvertToBaseUnit(quantity.Value);

        if (quantity.Unit is TemperatureUnit temperature)
            return temperature.ConvertToBaseUnit(quantity.Value);

        throw new NotSupportedException("Unit type not supported");
    }

    private double ConvertFromBaseUnit<U>(U unit, double baseValue)
        where U : struct, Enum
    {
        if (unit is LengthUnit length)
            return length.ConvertFromBaseUnit(baseValue);

        if (unit is WeightUnit weight)
            return weight.ConvertFromBaseUnit(baseValue);

        if (unit is VolumeUnit volume)
            return volume.ConvertFromBaseUnit(baseValue);

        if (unit is TemperatureUnit temperature)
            return temperature.ConvertFromBaseUnit(baseValue);

        throw new NotSupportedException("Unit type not supported");
    }

    public bool Compare<U>(QuantityModel<U> q1, QuantityModel<U> q2)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        return Math.Abs(base1 - base2) < EPSILON;
    }

    public QuantityModel<U> Add<U>(QuantityModel<U> q1, QuantityModel<U> q2)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        double resultBase = base1 + base2;

        double result = ConvertFromBaseUnit(q1.Unit, resultBase);

        return new QuantityModel<U>(result, q1.Unit);
    }


    public QuantityModel<U> Add<U>(
        QuantityModel<U> q1,
        QuantityModel<U> q2,
        U targetUnit)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        double resultBase = base1 + base2;

        double result = ConvertFromBaseUnit(targetUnit, resultBase);

        return new QuantityModel<U>(result, targetUnit);
    }

    public QuantityModel<U> Subtract<U>(QuantityModel<U> q1, QuantityModel<U> q2)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        double resultBase = base1 - base2;

        double result = ConvertFromBaseUnit(q1.Unit, resultBase);

        return new QuantityModel<U>(result, q1.Unit);
    }



    public QuantityModel<U> Subtract<U>(
        QuantityModel<U> q1,
        QuantityModel<U> q2,
        U targetUnit)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        double resultBase = base1 - base2;

        double result = ConvertFromBaseUnit(targetUnit, resultBase);

        return new QuantityModel<U>(result, targetUnit);
    }

    public double Divide<U>(QuantityModel<U> q1, QuantityModel<U> q2)
        where U : struct, Enum
    {
        if (q1 == null || q2 == null)
            throw new ArgumentException("Quantity cannot be null");

        double base1 = ConvertToBaseUnit(q1);
        double base2 = ConvertToBaseUnit(q2);

        if (Math.Abs(base2) < EPSILON)
            throw new ArithmeticException("Division by zero");

        return base1 / base2;
    }

    public QuantityModel<U> Convert<U>(QuantityModel<U> quantity, U targetUnit)
        where U : struct, Enum
    {
        if (quantity == null)
            throw new ArgumentException("Quantity cannot be null");

        double baseValue = ConvertToBaseUnit(quantity);

        double converted = ConvertFromBaseUnit(targetUnit, baseValue);

        return new QuantityModel<U>(converted, targetUnit);
    }
}