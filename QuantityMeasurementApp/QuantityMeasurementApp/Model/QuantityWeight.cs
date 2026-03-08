using System;

namespace QuantityMeasurementApp.Model;
    public class QuantityWeight
    {
        private readonly double value;
        private readonly WeightUnit unit;
        private const double EPSILON = 0.00001;

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid value");

            this.value = value;
            this.unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return unit.ConvertToBaseUnit(value);
        }

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double baseValue = ConvertToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityWeight(converted, targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            double sum = this.ConvertToBaseUnit() + other.ConvertToBaseUnit();

            double result = unit.ConvertFromBaseUnit(sum);

            return new QuantityWeight(result, unit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            double sum = this.ConvertToBaseUnit() + other.ConvertToBaseUnit();

            double result = targetUnit.ConvertFromBaseUnit(sum);

            return new QuantityWeight(result, targetUnit);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            QuantityWeight other = (QuantityWeight)obj;

            double base1 = this.ConvertToBaseUnit();
            double base2 = other.ConvertToBaseUnit();

            return Math.Abs(base1 - base2) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return value + " " + unit;
        }
    }
