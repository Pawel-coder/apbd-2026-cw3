// See https://aka.ms/new-console-template for more information
private static RentalService _rentalService = new();
bool endProgram = false;
while (!endProgram)
{
    Console.Clear();
    Console.WriteLine("Hardware Rental Service:");
    Console.WriteLine("1. Show all hardware");
    Console.WriteLine("2. Show all avaible hardware");
    Console.WriteLine("3. Borrow hardware");
    Console.WriteLine("4. Return hardware");
    Console.WriteLine("5. Change status of hardware");
    Console.WriteLine("6. Show active rentals for a user");
    Console.WriteLine("7. Show overdue rentals");
    Console.WriteLine("8. Show short report for rental service");
    Console.WriteLine("0. End program");
    Console.Write("\nPick your poison: ");
    
    var input = Console.ReadLine();
    Console.Clear();
    try
    {
        switch (input)
        {
            case "1":
                ShowAllHardware();
                break;
            case "2":
                ShowAvailableHardware();
                break;
            case "3":
                BorrowHardwareMenu();
                break;
            case "4":
                ReturnHardwareMenu();
                break;
            case "5":
                ChangeHardwareStatusMenu();
                break;
            case "6":
                ShowUserActiveRentalsMenu();
                break;
            case "7":
                ShowOverdueRentals();
                break;
            case "8":
                ShowReport();
                break;
            case "0":
                endProgram = true;
                break;
            default:
                Console.WriteLine("Not recognized input");
                break;
        }
    }
    catch (RentalException er)
    {
        Console.WriteLine($"\n[Business Error]: {er.Message}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"\n[Not recognized Error]: {e.Message}");
    }

    if (!endProgram)
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
