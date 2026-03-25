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
            Console.WriteLine("9. Add new user");
            Console.WriteLine("10. Add new hardware");
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
                    case "9":
                        AddNewUser();
                        break;
                    case "10":
                        AddNewHardware();
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
        foreach (var h in _rentalService.GetAvailableHardware())
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
        foreach (var u in _rentalService.GetUsers())
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

            var rentals = _rentalService.GetActiveRentalsForUser(userId);
            foreach (var r in rentals)
                Console.WriteLine($"Hardware: {r.RentedHardware.Name} Borrowed date: {r.BorrowDate:yyyy-MM-dd} Excpeted return date: {r.ExpectedReturnDate:yyyy-MM-dd}");
        }

        private static void ShowOverdueRentals()
        {
            Console.WriteLine("Overdue rentals");
            var rentals = _rentalService.GetOverdueRentals();
            foreach (var r in rentals)
                Console.WriteLine($"User: {r.Borrower.Surname} Hardware: {r.RentedHardware.Name} Expected return date: {r.ExpectedReturnDate:yyyy-MM-dd} Days over due date: {(DateTime.Now.Date - r.ExpectedReturnDate.Date).Days}");
        }

        private static void ShowReport()
        {
            Console.WriteLine(_rentalService.GenerateShortReport());
        }
        private static void AddNewUser()
        {
            Console.WriteLine("Adding new user");
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Employee");
            Console.Write("Pick type of user (1 or 2):");
            var typeChoice = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Surname: ");
            var surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                Console.WriteLine("Error - name or surname can't be empty");
                return;
            }

            User newUser;
            if (typeChoice == "1")
            {
                newUser = new Student(name, surname);
            }
            else if (typeChoice == "2")
            {
                newUser = new Employee(name, surname);
            }
            else
            {
                Console.WriteLine("Error - wrong user type");
                return;
            }

            _rentalService.RegisterUser(newUser);
            Console.WriteLine($"New user has been registered (ID: {newUser.ID}) {newUser.Name} {newUser.Surname}");
        }

        private static void AddNewHardware()
        {
            Console.WriteLine("Adding new hardware");
            Console.WriteLine("1. Laptop");
            Console.WriteLine("2. Projector");
            Console.WriteLine("3. Camera");
            Console.Write("Pick type of hardware (1, 2 or 3): ");
            var typeChoice = Console.ReadLine();

            Console.Write("Hardware name: ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error - name can't be empty");
                return;
            }

            Hardware newHardware = null;

            switch (typeChoice)
            {
                case "1":
                    Console.Write("How much RAM (GB)?: ");
                    if (!int.TryParse(Console.ReadLine(), out int ram)) { Console.WriteLine("Error: RAM ought to be number"); return; }
                    Console.Write("Input processor: ");
                    var processor = Console.ReadLine();
                    newHardware = new Laptop(name, 15.0m,processor, ram);
                    break;

                case "2":
                    Console.Write("Input brightness in lumen: ");
                    if (!int.TryParse(Console.ReadLine(), out int lumens)) { Console.WriteLine("Error - brithness ought to be number"); return; }
                    Console.Write("Input resolution: ");
                    var resolution = Console.ReadLine();
                    newHardware = new Projector(name, 5m, lumens, resolution);
                    break;

                case "3": 
                    Console.Write("How much zoom does camera have?: ");
                    if (!float.TryParse(Console.ReadLine(), out float zoom)) { Console.WriteLine("Error: zoom ought to be number"); return; }
                    Console.Write("How many Megapixels does sensor have?: ");
                    if (!float.TryParse(Console.ReadLine(), out float megapixels)) { Console.WriteLine("Error: megapixels ought to be number"); return; }
                    newHardware = new Camera(name, 10.0m, megapixels, zoom);
                    break;

                default:
                    Console.WriteLine("Error - wrong hardware type");
                    return;
            }

            _rentalService.RegisterHardware(newHardware);
            Console.WriteLine($"New hardware{newHardware.Name} (ID: {newHardware.ID}) has been added");
        }
}