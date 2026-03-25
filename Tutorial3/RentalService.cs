namespace Tutorial3;

public class RentalService
{
    private List<User> _users = [];
    private List<Hardware> _hardware = [];
    private List<Rental> _rentals = [];
    
    public void RegisterUser(User user)=>_users.Add(user);
    public void RegisterHardware(Hardware hardware) => _hardware.Add(hardware);
    
    public IEnumerable<User> GetUsers() => _users;
    public IEnumerable<Hardware> GetAllHardware() => _hardware;
    public IEnumerable<Hardware> GetAvailableHardware() => _hardware.Where(h=>h.Status == HardwareStatus.Available);

    public Rental BorrowHardware(Guid userId, Guid hardwareId, int days)
    {
        var user = _users.FirstOrDefault(u=>u.ID == userId);
        if (user == null)
        {
            throw new RentalException("User not found");
        }
        var hardware = _hardware.FirstOrDefault(h=>h.ID == hardwareId);
        if (hardware == null)
        {
            throw new RentalException("Hardware not found");
        }
        if(hardware.Status != HardwareStatus.Available)
            throw new RentalException("Hardware is not available to rent");
        var activeRentalCount = _rentals.Count(r => r.Borrower.ID == userId && r.IsOngoing);
        if(activeRentalCount >= user.ActiveRentalLimit)
            throw new RentalException($"Rental limit ({user.ActiveRentalLimit}) reached for user {user.Name}");
        hardware.Status = HardwareStatus.Borrowed;
        var rental = new Rental(user, hardware, DateTime.Now.AddDays(days));
        _rentals.Add(rental);
        return rental;
    }
}