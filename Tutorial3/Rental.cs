namespace Tutorial3;

public class Rental
{
    public Guid ID { get; }=Guid.NewGuid();
    public User Borrower { get; }
    public Hardware RentedHardware { get; }
    public DateTime BorrowDate { get; }
    public DateTime ExpectedReturnDate { get; }
    public DateTime? ActualReturnDate { get; private set; }
    public decimal Penalty { get; private set; }
    public bool IsOngoing => ActualReturnDate == null;
    public bool IsOverdue => IsOngoing && DateTime.Now.Date > ExpectedReturnDate;

    public Rental(User borrower, Hardware rentedHardware, DateTime borrowDate, DateTime expectedReturnDate)
    {
        Borrower = borrower;
        RentedHardware = rentedHardware;
        BorrowDate = borrowDate;
        ExpectedReturnDate = expectedReturnDate;
    }

    public void ReturnHardware(DateTime returnDate)
    {
        if(!IsOngoing)
            throw new RentalException("This rental has already been returned.");
        ActualReturnDate = returnDate;
        if (returnDate.Date > ExpectedReturnDate.Date)
        {
            int days = (returnDate.Date - ExpectedReturnDate.Date).Days;
            Penalty = days * RentedHardware.PenaltyRatePerDay;
        }
    }
}