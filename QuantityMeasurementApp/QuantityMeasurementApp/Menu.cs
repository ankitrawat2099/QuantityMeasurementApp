using QuantityMeasurementApp.Business.Interfaces;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Model;
namespace QuantityMeasurementApp;
    internal class Menu
    {
        public static void StartApp()
        {

            Console.WriteLine("1. Compare Length");
            Console.WriteLine("2. Convert Length");
            Console.WriteLine("3. Add Length");
            Console.WriteLine("4. Add Two units To Target Unit");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                IQuantityLength utility = new QuantityLengthUtility();

                Console.Write("Enter First Value: ");
                double value1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                Console.Write("Enter Second Value: ");
                double value2 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);
                bool result = utility.CheckEquality(value1, unit1, value2, unit2);
                Console.WriteLine("Equality Result: " + result);
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter Value:");
                double value = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter Source Unit (Feet/Inch/Yard/Centimeter):");
                LengthUnit source = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                Console.WriteLine("Enter Target Unit (Feet/Inch/Yard/Centimeter):");
                LengthUnit target = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                double result = QuantityLength.Convert(value, source, target);

                Console.WriteLine("Converted Value: " + result);
            }
            else if (choice == 3)
            {
                Console.Write("Enter First Value: ");
                double value1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                Console.Write("Enter Second Value: ");
                double value2 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                QuantityLength q1 = new QuantityLength(value1, unit1);
                QuantityLength q2 = new QuantityLength(value2, unit2);

                QuantityLength result = QuantityLength.AddTwoUnits(q1,q2);

                Console.WriteLine("Addition Result: " + result);
            }
            else if(choice == 4)
            {
                Console.Write("Enter First Value: ");
                double value1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                Console.Write("Enter Second Value: ");
                double value2 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                Console.Write("Enter Target Unit (Feet/Inch/Yard/Centimeter): ");
                LengthUnit targetUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

                QuantityLength q1 = new QuantityLength(value1, unit1);
                QuantityLength q2 = new QuantityLength(value2, unit2);

                QuantityLength result = QuantityLength.AddTwoUnits_TargetUnit(q1, q2, targetUnit);
                Console.WriteLine($"Result : {result}");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

        }
    }
