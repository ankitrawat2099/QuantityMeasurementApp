namespace QuantityMeasurementModelLayer.Entities
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        public double FirstValue { get; set; }
        public string FirstUnit { get; set; } = null!;

        public double SecondValue { get; set; }
        public string SecondUnit { get; set; } = null!;

        public string Operation { get; set; } = null!;

        public double Result { get; set; }

        public string MeasurementType { get; set; } = null!;

        public QuantityMeasurementEntity() { }

        public QuantityMeasurementEntity(
            double firstValue,
            string firstUnit,
            double secondValue,
            string secondUnit,
            string operation,
            double result,
            string measurementType)
        {
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            SecondValue = secondValue;
            SecondUnit = secondUnit;
            Operation = operation;
            Result = result;
            MeasurementType = measurementType;
        }
    }
}