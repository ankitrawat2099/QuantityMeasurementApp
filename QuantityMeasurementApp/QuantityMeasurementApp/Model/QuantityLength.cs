using System;

namespace QuantityMeasurementApp.Model;

public class QuantityLength
{
    private readonly double value;
    private readonly LengthUnit unit;
    public QuantityLength(double value, LengthUnit unit)
    {
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
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (this == obj) return true;
        if (!(obj is QuantityLength)) return false;
        QuantityLength other = (QuantityLength)obj;
        double thisInInch = this.ConvertToInch();
        double otherInInch = other.ConvertToInch();
        return thisInInch == otherInInch;
    }

    public override int GetHashCode()
    {
        return ConvertToInch().GetHashCode();
    }
}
