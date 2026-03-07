using System;

namespace QuantityMeasurementApp.Model;
    public class QuantityLength
    {
        private readonly double value;
        private readonly LengthUnit unit;
        private const double EPSILON = 0.0001;

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Value must be finite number.");

            this.value = value;
            this.unit = unit;
        }

        // Convert current object value to base unit (Feet)
        private double ConvertToBaseUnit()
        {
            return unit.ConvertToBaseUnit(value);
        }

        // UC5: Static Conversion API
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (source == target)
                return value;

            double baseValue = source.ConvertToBaseUnit(value);
            return target.ConvertFromBaseUnit(baseValue);
        }

        // Private addition helper
        private static double AddInBaseUnit(QuantityLength l1, QuantityLength l2)
        {
            return l1.ConvertToBaseUnit() + l2.ConvertToBaseUnit();
        }

        // UC6: Add result in first operand unit
        public QuantityLength Add(QuantityLength other)
        {
            if (other == null)
                throw new ArgumentException("Second operand cannot be null");

            double sumInBase = AddInBaseUnit(this, other);
            double resultValue = this.unit.ConvertFromBaseUnit(sumInBase);

            return new QuantityLength(resultValue, this.unit);
        }

        // UC6 static version
        public static QuantityLength AddTwoUnits(QuantityLength l1, QuantityLength l2)
        {
            if (l1 == null || l2 == null)
                throw new ArgumentException("Operands cannot be null");

            return l1.Add(l2);
        }

        // UC7: Add with explicit target unit
        public static QuantityLength AddTwoUnits_TargetUnit(
            QuantityLength l1,
            QuantityLength l2,
            LengthUnit targetUnit)
        {
            if (l1 == null || l2 == null)
                throw new ArgumentException("Operands cannot be null");

            double sumInBase = AddInBaseUnit(l1, l2);
            double resultValue = targetUnit.ConvertFromBaseUnit(sumInBase);

            return new QuantityLength(resultValue, targetUnit);
        }

        // Optional UC8 instance-style convert
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double convertedValue = Convert(this.value, this.unit, targetUnit);
            return new QuantityLength(convertedValue, targetUnit);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is QuantityLength)) return false;

            QuantityLength other = (QuantityLength)obj;

            return Math.Abs(this.ConvertToBaseUnit() - other.ConvertToBaseUnit()) < EPSILON;
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
