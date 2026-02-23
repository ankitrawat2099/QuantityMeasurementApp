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
    // Static method to convert between units
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

    // Method to add two QuantityLength objects
    public QuantityLength Add(QuantityLength other)
    {
        if (other == null)
            throw new ArgumentException("Second operand cannot be null");

        double thisInInch = this.ConvertToInch();
        double otherInInch = other.ConvertToInch();

        double sumInInch = thisInInch + otherInInch;

        double resultValue;

        if (this.unit == LengthUnit.Feet)
            resultValue = sumInInch / 12;
        else if (this.unit == LengthUnit.Inch)
            resultValue = sumInInch;
        else if (this.unit == LengthUnit.Yard)
            resultValue = sumInInch / 36;
        else if (this.unit == LengthUnit.Centimeter)
            resultValue = sumInInch / 0.393701;
        else
            throw new ArgumentException("Invalid unit");

        return new QuantityLength(resultValue, this.unit);
    }
    // Static method to add two QuantityLength objects
    public static QuantityLength Add(QuantityLength l1, QuantityLength l2)
    {
        if (l1 == null || l2 == null)
            throw new ArgumentException("Operands cannot be null");

        return l1.Add(l2);
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
    public override string ToString()
    {
        return value + " " + unit;
    }
}
