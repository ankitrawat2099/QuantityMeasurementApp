using System;

namespace QuantityMeasurementApp.Model;
    public class QuantityVolume
    {
        public double Value { get; }
        public VolumeUnit Unit { get; }

        public QuantityVolume(double value, VolumeUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is QuantityVolume))
                return false;

            QuantityVolume other = (QuantityVolume)obj;

            double v1 = ConvertTo(VolumeUnit.LITRE).Value;
            double v2 = other.ConvertTo(VolumeUnit.LITRE).Value;

            return Math.Abs(v1 - v2) < 0.0001;
        }

        public QuantityVolume ConvertTo(VolumeUnit target)
        {
            double baseValue = Value * Unit.ToBaseUnit();
            double converted = baseValue / target.ToBaseUnit();

            return new QuantityVolume(converted, target);
        }

        public QuantityVolume Add(QuantityVolume other)
        {
            double converted = other.ConvertTo(this.Unit).Value;

            return new QuantityVolume(this.Value + converted, this.Unit);
        }
    }
