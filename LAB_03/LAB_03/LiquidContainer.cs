namespace LAB_03
{
    public class LiquidContainer : Container
    {
        public bool IsHazardous { get; private set; }

        public LiquidContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, bool isHazardous)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            IsHazardous = isHazardous;
        }

        public override string GetContainerType()
        {
            return "Liquid";
        }
    }
}