using System;

namespace QuantityMeasurementApp.Model;

public class QuantityLength
{
    private readonly double value;
    private readonly LengthUnit unit;
    private const double EPSILON = 0.0001;

    public QuantityLength(double value, LengthUnit unit)
    {
        if (Double.IsNaN(value) || Double.IsInfinity(value))
            throw new ArgumentException("Value must be finite number.");

        this.value = value;
        this.unit = unit;
    }

    //base conversion helpers

    // Convert current object to base unit (Inch)
    private double ConvertToInch()
    {
        return Convert(value, unit, LengthUnit.Inch);
    }


    // UC5: Static Conversion API


    public static double Convert(double value, LengthUnit source, LengthUnit target)
    {
        if (source == target)
            return value;

        double valueInInch;

        // Convert source → Inch (Base Unit)
        if (source == LengthUnit.Feet)
            valueInInch = value * 12;
        else if (source == LengthUnit.Inch)
            valueInInch = value;
        else if (source == LengthUnit.Yard)
            valueInInch = value * 36;
        else if (source == LengthUnit.Centimeter)
            valueInInch = value * 0.393701;
        else
            throw new ArgumentException("Invalid source unit");

        // Convert Inch → Target
        if (target == LengthUnit.Feet)
            return valueInInch / 12;
        else if (target == LengthUnit.Inch)
            return valueInInch;
        else if (target == LengthUnit.Yard)
            return valueInInch / 36;
        else if (target == LengthUnit.Centimeter)
            return valueInInch / 0.393701;
        else
            throw new ArgumentException("Invalid target unit");
    }


    // PRIVATE ADDITION HELPER (DRY)


    private static double AddInBaseUnit(QuantityLength l1, QuantityLength l2)
    {
        return l1.ConvertToInch() + l2.ConvertToInch();
    }


    // UC6: Add (Result in First Operand Unit)


    public QuantityLength Add(QuantityLength other)
    {
        if (other == null)
            throw new ArgumentException("Second operand cannot be null");

        double sumInInch = AddInBaseUnit(this, other);

        double resultValue = Convert(sumInInch, LengthUnit.Inch, this.unit);

        return new QuantityLength(resultValue, this.unit);
    }


    // UC6 Static Version


    public static QuantityLength AddTwoUnits(QuantityLength l1, QuantityLength l2)
    {
        if (l1 == null || l2 == null)
            throw new ArgumentException("Operands cannot be null");

        return l1.Add(l2);
    }


    // UC7: Add With Explicit Target Unit


    public static QuantityLength AddTwoUnits_TargetUnit(
        QuantityLength l1,
        QuantityLength l2,
        LengthUnit targetUnit)
    {
        if (l1 == null || l2 == null)
            throw new ArgumentException("Operands cannot be null");

        double sumInInch = AddInBaseUnit(l1, l2);

        double resultValue = Convert(sumInInch, LengthUnit.Inch, targetUnit);

        return new QuantityLength(resultValue, targetUnit);
    }

    // Equality Override


    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (this == obj) return true;
        if (!(obj is QuantityLength)) return false;

        QuantityLength other = (QuantityLength)obj;

        return Math.Abs(this.ConvertToInch() - other.ConvertToInch()) < EPSILON;
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
