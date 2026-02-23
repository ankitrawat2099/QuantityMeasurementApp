    namespace QuantityMeasurementApp;
    internal class Menu
    {
        public static void StartApp()
        {
            int input;
            do
            {
                Console.WriteLine("\nPress 0. To Exit");
                Console.WriteLine("Press 1. For checking Feet Equality");
                while (!int.TryParse(Console.ReadLine(), out input)) Console.WriteLine("Invalid Input : ");
                if (input == 0) return;
                if (input == 1)
                {// creating object for FeetUtility class
                    IFeet obj = new FeetUtility();
                    double Feet_1;// input variable for first value
                    double Feet_2;// input variable for second value
                    Console.Write("Enter first value : ");
                    while (!double.TryParse(Console.ReadLine(), out Feet_1)){
                        Console.WriteLine("Invalid Input : ");// validating input for first value
                    }
                    Console.Write("Enter Second value : ");
                    while (!double.TryParse(Console.ReadLine(), out Feet_2))
                    {
                        Console.WriteLine("Invalid Input : ");// validating input for second value
                    } 
                    obj.CompareFeet(Feet_1, Feet_2);
                }
            } while (true);
        }
    }
