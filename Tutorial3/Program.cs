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
        foreach (var h in _rentalService.GetAvailableHardware())
            Console.WriteLine($"{h.ID} | {h}");
    }
    private static void BorrowHardwareMenu()
    {
        Console.WriteLine("Borrowed Hardware");
        Console.WriteLine("User list:");
        foreach (var u in _rentalService.GetAllUsers())
            Console.WriteLine($"({u.ID}) {u}");
            
        Console.Write("\nInput user ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid userId)) return;

        Console.WriteLine("\nLista Dostępnego Sprzętu:");
        foreach (var h in _rentalService.GetAvailableHardware())
            Console.WriteLine($"({h.ID}) {h.Name} ");

        Console.Write("\nInput hardware ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid hID)) return;

        Console.Write("For how many days do you rent? ");
        if (!int.TryParse(Console.ReadLine(), out int days)) return;

        var rental = _rentalService.BorrowHardware(userId, hID, days);
        Console.WriteLine($"\nHardware has been rented. Expected return date: {rental.ExpectedReturnDate:yyyy-MM-dd}");
    }
    private static void ReturnHardwareMenu()
        {
            Console.WriteLine("Return Hardware");
            Console.Write("Input user ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid userId)) return;

            var rentals = _rentalService.GetActiveRentalsForUser(userId).ToList();
            if (!rentals.Any())
            {
                Console.WriteLine("No active rentals this user has");
                return;
            }

            foreach (var r in rentals)
                Console.WriteLine($"Rental ID: {r.ID} Name: {r.RentedHardware.Name} Expected Date: {r.ExpectedReturnDate:yyyy-MM-dd}");

            Console.Write("\nInput hardware ID to return: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid rentalId)) return;

            var returnedRental = _rentalService.Return(rentalId);
            Console.WriteLine($"\nHardware returned");
            if (returnedRental.Penalty > 0)
                Console.WriteLine($"\nBEWARE: Penalty fee has been issued! : {returnedRental.Penalty:C}");
        }

        private static void ChangeHardwareStatusMenu()
        {
            Console.WriteLine("Changing hardware status");
            ShowAllHardware();
            Console.Write("\nInput hardware ID to change status: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid hId)) return;

            Console.WriteLine("1. Avaible\n2. Unavaible");
            Console.Write("Pick status: ");
            var choice = Console.ReadLine();
            
            var status = choice == "2" ? HardwareStatus.Unavailable : HardwareStatus.Available;
            _rentalService.ChangeHardwareStatus(hId, status);
            Console.WriteLine("Status changed");
        }

        private static void ShowUserActiveRentalsMenu()
        {
            Console.WriteLine("Active rentals for user");
            Console.Write("Input user ID: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid userId)) return;

            var rentals = _rentalService.GetActiveHardwareForUser(userId);
            foreach (var r in rentals)
                Console.WriteLine($"Hardware: {r.RentedHardware.Name} Borrowed date: {h.BorrowDate:yyyy-MM-dd} Excpeted return date: {h.ExpectedReturnDate:yyyy-MM-dd}");
        }

        private static void ShowOverdueRentals()
        {
            Console.WriteLine("Overdue rentals");
            var rentals = _rentalService.GetOverdueLoans();
            foreach (var r in rentals)
                Console.WriteLine($"User: {r.Borrower.LastName} Hardware: {r.RentedEquipment.Name} Expected return date: {r.ExpectedReturnDate:yyyy-MM-dd} Days over due date: {(DateTime.Now.Date - r.ExpectedReturnDate.Date).Days}");
        }

        private static void ShowReport()
        {
            Console.WriteLine(_rentalService.GenerateReport());
        }
}