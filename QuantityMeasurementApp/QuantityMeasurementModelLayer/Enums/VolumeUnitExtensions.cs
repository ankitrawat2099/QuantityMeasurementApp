using System;
using QuantityMeasurementModelLayer.Enums;

public static class VolumeUnitExtensions
{
    public static double ToBaseUnit(this VolumeUnit unit)
    {
        switch (unit)
        {
            case VolumeUnit.LITRE:
                return 1.0;

            case VolumeUnit.MILLILITRE:
                return 0.001;

            case VolumeUnit.GALLON:
                return 3.78541;

            default:
                throw new ArgumentException("Invalid Volume Unit");
        }
    }
}