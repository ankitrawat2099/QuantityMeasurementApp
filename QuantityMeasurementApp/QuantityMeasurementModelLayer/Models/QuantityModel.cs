namespace QuantityMeasurementModelLayer.Models;

public class QuantityModel<U> where U : struct
{
    public double Value { get; }
    public U Unit { get; }

    public QuantityModel(double value, U unit)
    {
        Value = value;
        Unit = unit;
    }

    public override string ToString()
    {
        return $"{Value} {Unit}";
    }
}