using System;

namespace QuantityMeasurementApp.Model;

public class Quantity<U> where U : struct
{
    private readonly double value;
    private readonly U unit;
    private const double EPSILON = 0.000001;

    public Quantity(double value, U unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("Invalid value");

        this.value = value;
        this.unit = unit;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Quantity<U>))
            return false;

        Quantity<U> other = (Quantity<U>)obj;

        double base1 = ConvertToBase(this.value, this.unit);
        double base2 = ConvertToBase(other.value, other.unit);

        return Math.Abs(base1 - base2) < EPSILON;
    }

    private double ConvertToBase(double value, U unit)
    {
        dynamic u = unit;
        return u.ConvertToBaseUnit(value);
    }

    public Quantity<U> ConvertTo(U targetUnit)
    {
        double baseValue = ConvertToBase(this.value, this.unit);

        dynamic t = targetUnit;
        double converted = t.ConvertFromBaseUnit(baseValue);

        return new Quantity<U>(converted, targetUnit);
    }

    public Quantity<U> Add(Quantity<U> other, U targetUnit)
    {
        double base1 = ConvertToBase(this.value, this.unit);
        double base2 = ConvertToBase(other.value, other.unit);

        double sum = base1 + base2;

        dynamic t = targetUnit;
        double result = t.ConvertFromBaseUnit(sum);

        return new Quantity<U>(result, targetUnit);
    }

    public Quantity<U> Subtract(Quantity<U> other)
    {
        if (other == null)
            throw new ArgumentException("Quantity cannot be null");

        double baseValue1 = ConvertToBase(this.value, this.unit);
        double baseValue2 = ConvertToBase(other.value, other.unit);

        double baseResult = baseValue1 - baseValue2;

        dynamic u = this.unit;
        double result = u.ConvertFromBaseUnit(baseResult);

        result = Math.Round(result, 2);

        return new Quantity<U>(result, unit);
    }
    public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
    {
        if (other == null)
            throw new ArgumentException("Quantity cannot be null");

        double baseValue1 = ConvertToBase(this.value, this.unit);
        double baseValue2 = ConvertToBase(other.value, other.unit);

        double baseResult = baseValue1 - baseValue2;

        dynamic t = targetUnit;
        double result = t.ConvertFromBaseUnit(baseResult);

        result = Math.Round(result, 2);

        return new Quantity<U>(result, targetUnit);
    }
    public double Divide(Quantity<U> other)
    {
        if (other == null)
            throw new ArgumentException("Quantity cannot be null");

        double baseValue1 = ConvertToBase(this.value, this.unit);
        double baseValue2 = ConvertToBase(other.value, other.unit);

        if (baseValue2 == 0)
            throw new ArithmeticException("Division by zero");

        return baseValue1 / baseValue2;
    }

    public override int GetHashCode()
    {
        double baseValue = ConvertToBase(value, unit);
        return baseValue.GetHashCode();
    }

    public override string ToString()
    {
        return $"Quantity({value}, {unit})";
    }
}

