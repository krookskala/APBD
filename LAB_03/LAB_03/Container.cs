namespace LAB_03;

public abstract class Container
{
    public string SerialNumber { get; private set; }
    public double CargoMass { get; private set; }
    public double Height { get; private set; }
    public double Depth { get; private set; }
    public double TareWeight { get; private set; }
    public double MaximumPayload { get; private set; }

    public Container(string serialNumber, double height, double depth, double tareWeight, double maximumPayload)
    {
        SerialNumber = serialNumber;
        Height = height;
        Depth = depth;
        TareWeight = tareWeight;
        MaximumPayload = maximumPayload;
    }

    public LoadCargo(double cargoMass)
    {
        if (cargoMass > MaximumPayload)
            throw new OverfillException();
        CargoMass = cargoMass;
    }

    public void EmptyCargo()
    {
        CargoMass = 0;
    }

    protected string GenerateSerialNumber()
    {
        throw new NotImplementedException();
    }
}