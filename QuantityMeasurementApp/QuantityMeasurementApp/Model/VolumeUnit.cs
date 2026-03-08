namespace QuantityMeasurementApp.Model;
    public enum VolumeUnit
    {
        LITRE,
        MILLILITRE,
        GALLON
    }

    public static class VolumeUnitExtension
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
