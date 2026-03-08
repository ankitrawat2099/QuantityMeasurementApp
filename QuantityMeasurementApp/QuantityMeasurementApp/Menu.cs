using System;
using QuantityMeasurementApp.Model;

namespace QuantityMeasurementApp;

internal class Menu
{
public static void StartApp()
{
Console.WriteLine("===== Quantity Measurement App =====");
Console.WriteLine("1. Compare Length");
Console.WriteLine("2. Convert Length");
Console.WriteLine("3. Add Length");
Console.WriteLine("4. Add Two Units To Target Unit");

    Console.Write("Enter choice: ");
    int choice = Convert.ToInt32(Console.ReadLine());

    if (choice == 1)
    {
        Console.Write("Enter First Value: ");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        Console.Write("Enter Second Value: ");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        QuantityLength q1 = new QuantityLength(value1, unit1);
        QuantityLength q2 = new QuantityLength(value2, unit2);

        bool result = q1.Equals(q2);

        Console.WriteLine("Equality Result: " + result);
    }
    else if (choice == 2)
    {
        Console.Write("Enter Value: ");
        double value = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Source Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit source = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        Console.Write("Enter Target Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit target = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        double result = QuantityLength.Convert(value, source, target);

        Console.WriteLine("Converted Value: " + result);
    }
    else if (choice == 3)
    {
        Console.Write("Enter First Value: ");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        Console.Write("Enter Second Value: ");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        QuantityLength q1 = new QuantityLength(value1, unit1);
        QuantityLength q2 = new QuantityLength(value2, unit2);

        QuantityLength result = q1.Add(q2);

        Console.WriteLine("Addition Result: " + result.Value + " " + result.Unit);
    }
    else if (choice == 4)
    {
        Console.Write("Enter First Value: ");
        double value1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit1 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        Console.Write("Enter Second Value: ");
        double value2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit unit2 = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        Console.Write("Enter Target Unit (FEET/INCHES/YARDS/CENTIMETERS): ");
        LengthUnit targetUnit = (LengthUnit)Enum.Parse(typeof(LengthUnit), Console.ReadLine(), true);

        QuantityLength q1 = new QuantityLength(value1, unit1);
        QuantityLength q2 = new QuantityLength(value2, unit2);

        QuantityLength result =
            QuantityLength.AddTwoUnits_TargetUnit(q1, q2, targetUnit);

        Console.WriteLine("Result: " + result.Value + " " + result.Unit);
    }
    else
    {
        Console.WriteLine("Invalid Choice");
    }
}

}
