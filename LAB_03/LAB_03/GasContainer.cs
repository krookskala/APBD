namespace LAB_03
{
    public class GasContainer : Container
    {
        public double Pressure { get; private set; }

        public GasContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, double pressure)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            Pressure = pressure;
        }

        public override string GetContainerType()
        {
            return "Gas";
        }
    }

}