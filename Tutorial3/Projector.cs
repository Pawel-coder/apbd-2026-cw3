namespace Tutorial3;

public class Projector : Hardware
{
    public int Brightness {get;set;}
    public string Resolution {get;set;}

    public Projector(string name, decimal penaltyRatePerDay, int brightness, string resolution) : base(name, penaltyRatePerDay)
    {
        Brightness = brightness;
        Resolution = resolution;
    }

    public override string GetShortDescription()
    {
        return $"Projector: {Brightness} lm, {Resolution}";
    }
}