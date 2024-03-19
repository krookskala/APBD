namespace LAB_03;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double RequiredTemperature { get; private set; }

    public RefrugeratedContainer(double maximumPayload, double tareWeight, double height, double depth,
        string productType, double requiredTemperature) : base(maximumPayload, tareWeight, height, depth)
    {
        ProductType = productType;
        RequiredTemperature = requiredTemperature;
    }
}