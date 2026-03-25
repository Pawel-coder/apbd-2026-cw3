namespace Tutorial3;

public abstract class Hardware
{
    public Guid ID { get; }=Guid.NewGuid();
    public string Name { get; set; }
    public HardwareStatus Status { get; set; } = HardwareStatus.Available;
    public decimal PenaltyRatePerDay { get; protected set; }

    protected Hardware(string name, decimal penaltyRatePerDay)
    {
        Name = name;
        PenaltyRatePerDay = penaltyRatePerDay;
    }
    public abstract string GetShortDescription();
    public override string ToString()
    {
        return $"[{ID}] {Name} (STATUS: {Status}) - {GetShortDescription()}";
    }
}