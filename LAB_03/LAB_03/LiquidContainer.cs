namespace LAB_03
{
    public class LiquidContainer : Container, IHazardNotification
    {
        public bool IsHazardous { get; private set; }

        public LiquidContainer(double cargoMass,string cargoUnit, int height, double tareWeight, int depth, string serialNumber, double maxPayload, bool isHazardous)
            : base(cargoMass, "L", height, tareWeight, depth, serialNumber, maxPayload)
        {
            IsHazardous = isHazardous;
        }
        
        public void NotifyHazard()
        {
            if (IsHazardous)
            {
                Console.WriteLine($"Liquid Container {SerialNumber} Contains Hazardous Material.");
            }
        }

        public override void LoadCargo(double mass)
        {
            double maxAllowedPayload = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
            if (mass + CargoMass > maxAllowedPayload)
            {
                Console.WriteLine($"Cannot Load {mass} {CargoUnit} Because It Exceeds The Maximum Allowed Capacity. Current Cargo Mass: {CargoMass} {CargoUnit}, Maximum Allowed Payload: {maxAllowedPayload} {CargoUnit}.");
                throw new OverfillException($"Loading The Cargo Would Exceed The Container's Allowed Payload For {(IsHazardous ? "Hazardous" : "Non-Hazardous")} Cargo.");
            }
            CargoMass += mass;
            Console.WriteLine($"Cargo Loaded Successfully. New Cargo Mass: {CargoMass} {CargoUnit}.");
        }
        
        public override string GetContainerType()
        {
            return "Liquid";
        }
    }
}