namespace Tutorial3;

public class Camera : Hardware
{
    public float SensorResolution { get; set; }
    public float OpticZoom {get; set;}

    public Camera(string name, decimal penaltyRatePerDay, float sensorResolution, float opticZoom) : base(name, penaltyRatePerDay)
    {
        SensorResolution = sensorResolution;
        OpticZoom = opticZoom;
    }
    
    public override string GetShortDescription()
    {
        return $"Camera: {SensorResolution} Megapixels, {OpticZoom}X optical zoom";
    }
}