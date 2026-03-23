using System;

namespace QuantityMeasurementModelLayer.Enums
{
    public static class VolumeUnitExtensions
    {
        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            switch (unit)
            {
                case VolumeUnit.LITRE:
                    return value;

                case VolumeUnit.MILLILITRE:
                    return value * 0.001;

                case VolumeUnit.GALLON:
                    return value * 3.78541;

                default:
                    throw new ArgumentException("Invalid Volume Unit");
            }
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double value)
        {
            switch (unit)
            {
                case VolumeUnit.LITRE:
                    return value;

                case VolumeUnit.MILLILITRE:
                    return value / 0.001;

                case VolumeUnit.GALLON:
                    return value / 3.78541;

                default:
                    throw new ArgumentException("Invalid Volume Unit");
            }
        }
    }
}