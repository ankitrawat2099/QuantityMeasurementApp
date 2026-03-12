using System;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;

public class Menu
{
    private readonly IQuantityMeasurementService service;

    public Menu(IQuantityMeasurementService service)
    {
        this.service = service;
    }

    public void Start()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n===== Quantity Measurement System =====");
                Console.WriteLine("1 Length");
                Console.WriteLine("2 Volume");
                Console.WriteLine("3 Weight");
                Console.WriteLine("4 Temperature");
                Console.WriteLine("5 Exit");

                Console.Write("Enter choice: ");
                int type = Convert.ToInt32(Console.ReadLine());

                if (type == 5)
                    return;

                OperationMenu(type);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // -------- OPERATION MENU --------

    private void OperationMenu(int type)
{
    Console.WriteLine("\nSelect Operation");
    Console.WriteLine("1 Add");
    Console.WriteLine("2 Subtract");
    Console.WriteLine("3 Divide");
    Console.WriteLine("4 Compare");
    Console.WriteLine("5 Convert");

    Console.Write("Enter Operation: ");
    int operation = Convert.ToInt32(Console.ReadLine());

    QuantityDTO q1 = GetQuantity(type);

    if (operation == 5)
    {
        ConvertUnit(q1, type);
        return;
    }

    QuantityDTO q2 = GetQuantity(type);

    switch (operation)
    {
        case 1:
            PrintResult(service.Add(q1, q2));
            break;

        case 2:
            PrintResult(service.Subtract(q1, q2));
            break;

        case 3:

double result = service.Divide(q1, q2);
Console.WriteLine("Result = " + result);

break;

        case 4:
            Console.WriteLine("Are Equal = " + service.Compare(q1, q2));
            break;

        default:
            Console.WriteLine("Invalid Operation");
            break;
    }
}
    // -------- INPUT --------

   private QuantityDTO GetQuantity(int type)
{
    Console.Write("\nEnter Value: ");
    double value = Convert.ToDouble(Console.ReadLine());

    string unit = SelectUnit(type);

    return new QuantityDTO(value, unit);
}
    // -------- UNIT MENU --------

   
        private string SelectUnit(int type)
{
    Console.WriteLine("Select Unit");

    if (type == 1) // Length
    {
        Console.WriteLine("1 FEET");
        Console.WriteLine("2 INCHES");
        Console.WriteLine("3 YARDS");
        Console.WriteLine("4 CENTIMETERS");

        int choice = Convert.ToInt32(Console.ReadLine());

        return choice switch
        {
            1 => "FEET",
            2 => "INCHES",
            3 => "YARDS",
            4 => "CENTIMETERS",
            _ => "FEET"
        };
    }

    else if (type == 2) // Volume
    {
        Console.WriteLine("1 LITRE");
        Console.WriteLine("2 MILLILITRE");
        Console.WriteLine("3 GALLON");

        int choice = Convert.ToInt32(Console.ReadLine());

        return choice switch
        {
            1 => "LITRE",
            2 => "MILLILITRE",
            3 => "GALLON",
            _ => "LITRE"
        };
    }

    else if (type == 3) // Weight
    {
        Console.WriteLine("1 KILOGRAM");
        Console.WriteLine("2 GRAM");
        Console.WriteLine("3 POUND");

        int choice = Convert.ToInt32(Console.ReadLine());

        return choice switch
        {
            1 => "KILOGRAM",
            2 => "GRAM",
            3 => "POUND",
            _ => "KILOGRAM"
        };
    }

    else // Temperature
    {
        Console.WriteLine("1 CELSIUS");
        Console.WriteLine("2 FAHRENHEIT");

        int choice = Convert.ToInt32(Console.ReadLine());

        return choice switch
        {
            1 => "CELSIUS",
            2 => "FAHRENHEIT",
            _ => "CELSIUS"
        };
    }
}
    // -------- CONVERT --------

    private void ConvertUnit(QuantityDTO q1, int type)
{
    Console.WriteLine("\nSelect Target Unit");

    string targetUnit = SelectUnit(type);

    QuantityDTO result = service.Convert(q1, targetUnit);

    PrintResult(result);
}

    // -------- OUTPUT --------

    private void PrintResult(QuantityDTO result)
    {
        Console.WriteLine($"\nResult = {result.Value} {result.Unit}");
    }
}