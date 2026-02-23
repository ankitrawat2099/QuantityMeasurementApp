namespace QuantityMeasurementApp;
internal class FeetUtility : IFeet{
        public void CompareFeet(double input1, double input2)
        {
            Feet f1 = new Feet(input1);
            Feet f2 = new Feet(input2);
            if(f1.Equals(f2))Console.WriteLine("Equals");
            else Console.WriteLine("Not Equals");
        }
    }
