namespace Tutorial3;

public class Laptop : Hardware
{
    public string CPU { get; set; }
    public int RAMGB { get; set; }

    public Laptop(string name, decimal penaltyRatePerDay, string cpu, int ramGB) : base(name, penaltyRatePerDay)
    {
        CPU = cpu;
        RAMGB = ramGB;
    }

    public override string GetShortDescription()
    {
        return $"Laptop: {CPU}, {RAMGB} GB RAM";
    }
}