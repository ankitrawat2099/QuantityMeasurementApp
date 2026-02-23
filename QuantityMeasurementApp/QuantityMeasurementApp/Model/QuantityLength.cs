using System;

namespace QuantityMeasurementApp.Model;

public class QuantityLength
{
    private readonly double value;
    private readonly LengthUnit unit;
    public QuantityLength(double value, LengthUnit unit)
    {
        if (Double.IsNaN(value) || Double.IsInfinity(value))
            throw new ArgumentException("Value must be finite number.");
        this.value = value;
        this.unit = unit;
    }

    private double ConvertToInch()
    {
        if (unit == LengthUnit.Feet)
            return value * 12;
        if (unit == LengthUnit.Inch)
            return value;
        if (unit == LengthUnit.Yard)
            return value * 36;
        if (unit == LengthUnit.Centimeter)
            return value * 0.393701;
        throw new ArgumentException("Invalid unit");
    }
    public static double Convert(double value, LengthUnit source, LengthUnit target)
    {
        if (source == target)
            return value;

        QuantityLength temp = new QuantityLength(value, source);
        double valueInInch = temp.ConvertToInch();

        if (target == LengthUnit.Feet)
            return valueInInch / 12;
        if (target == LengthUnit.Inch)
            return valueInInch;
        if (target == LengthUnit.Yard)
            return valueInInch / 36;
        if (target == LengthUnit.Centimeter)
            return valueInInch / 0.393701;
        throw new ArgumentException("Invalid target unit");
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (this == obj) return true;
        if (!(obj is QuantityLength)) return false;
        QuantityLength other = (QuantityLength)obj;
        double thisInInch = this.ConvertToInch();
        double otherInInch = other.ConvertToInch();
        return Math.Abs(thisInInch - otherInInch) < 0.000001;
    }

    public override int GetHashCode()
    {
        return ConvertToInch().GetHashCode();
    }
}
