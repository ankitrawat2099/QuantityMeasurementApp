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

        public string ResultUnit { get; set; } = null!;

        public string MeasurementType { get; set; } = null!;

        public int UserId { get; set; }
        public UserEntity? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public QuantityMeasurementEntity() { }

        public QuantityMeasurementEntity(
            double firstValue,
            string firstUnit,
            double secondValue,
            string secondUnit,
            string operation,
            double result,
            string resultUnit,
            string measurementType,
            int userId)
        {
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            SecondValue = secondValue;
            SecondUnit = secondUnit;
            Operation = operation;
            Result = result;
            ResultUnit = resultUnit;
            MeasurementType = measurementType;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}