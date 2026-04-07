namespace QuantityMeasurementModelLayer.DTO;
public class ConvertRequestDTO
    {
        public QuantityDTO QuantityDTO { get; set; } = null!;
        public string TargetUnit { get; set; } = null!;
    }