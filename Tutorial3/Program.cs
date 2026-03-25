namespace Tutorial3;

public class Program
{
    private static RentalService _rentalService = new();
    public static void Main(string[] args)
    {
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
    }
    private static void ShowAllHardware()
    {
        Console.WriteLine("All Hardware: ");
        foreach (var h in _rentalService.GetAvaibleHardware())
            Console.WriteLine($"{h.ID} | {h}");
    }
    private static void ShowAvailableHardware()
    {
        Console.WriteLine("Avaible Hardware: ");
        foreach (var eq in _rentalService.GetAvailableHardware())
            Console.WriteLine($"{h.Id} | {h}");
    }
    private static void BorrowHardwareMenu()
    {
        Console.WriteLine("Borrowed Hardware");
        Console.WriteLine("User list:");
        foreach (var u in _rentalService.GetAllUsers())
            Console.WriteLine($"{u.Id} | {u}");
            
        Console.Write("\nInput user ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid userId)) return;

        Console.WriteLine("\nLista Dostępnego Sprzętu:");
        foreach (var h in _rentalService.GetAvailableHardware())
            Console.WriteLine($"({h.ID}) {h.Name} ");

        Console.Write("\nInput hardware ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid hID)) return;

        Console.Write("For how many days do you rent? ");
        if (!int.TryParse(Console.ReadLine(), out int days)) return;

        var rental = _rentalService.BorrowHardware(userId, hId, days);
        Console.WriteLine($"\nHardware has been rented. Expected return date: {rental.ExpectedReturnDate:yyyy-MM-dd}");
    }
}