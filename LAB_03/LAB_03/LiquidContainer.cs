namespace LAB_03
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; set; }
        public LiquidContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, bool isHazardous)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double mass)
        {
            var maxLoad = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
            if (mass > maxLoad)
            {
                NotifyHazard();
                throw new OverfillException("Cargo Exceeds Maximum Allowable Payload.");
            }
            CargoMass = mass;
        }

        public override void EmptyCargo()
        {
            CargoMass = 0;
        }

        public void NotifyHazard()
        {
            if (IsHazardous)
            {
                Console.WriteLine($"Hazardous Cargo In Container {SerialNumber}.");
            }
        }
    }
}