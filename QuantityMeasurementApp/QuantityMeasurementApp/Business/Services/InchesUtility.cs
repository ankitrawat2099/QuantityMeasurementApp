using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Model;
namespace QuantityMeasurementApp.Business.Services
{
    public class InchesUtility : IInches
    {
        public void CompareInches(double inches1, double inches2)
        {
            Inches obj1 = new Inches(inches1);
            Inches obj2 = new Inches(inches2);
            if (obj1.Equals(obj2))Console.WriteLine("Both are Equal");
            else Console.WriteLine("Both are Not Equal");
        }
    }
}