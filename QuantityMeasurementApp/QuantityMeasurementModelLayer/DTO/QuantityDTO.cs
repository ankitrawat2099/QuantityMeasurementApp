namespace QuantityMeasurementModelLayer.DTO
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string MeasurementType { get; set; } 

        public QuantityDTO(double value, string unit, string measurementType)
        {
            Value = value;
            Unit = unit;
            MeasurementType = measurementType;
        }
    }
}