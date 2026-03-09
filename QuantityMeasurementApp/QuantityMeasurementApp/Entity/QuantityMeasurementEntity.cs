namespace QuantityMeasurementApp.Entity;

public class QuantityMeasurementEntity
{
    public string Operation { get; set; }

    public double? Value1 { get; set; }
    public string Unit1 { get; set; }

    public double? Value2 { get; set; }
    public string Unit2 { get; set; }

    public double? Result { get; set; }

    public bool HasError { get; set; }

    public string ErrorMessage { get; set; }

    public QuantityMeasurementEntity(
        string operation,
        double? value1,
        string unit1,
        double? value2,
        string unit2,
        double? result)
    {
        Operation = operation;
        Value1 = value1;
        Unit1 = unit1;
        Value2 = value2;
        Unit2 = unit2;
        Result = result;
    }

    public QuantityMeasurementEntity(string operation, string error)
    {
        Operation = operation;
        HasError = true;
        ErrorMessage = error;
    }
}