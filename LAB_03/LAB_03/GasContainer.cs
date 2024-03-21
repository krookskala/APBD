namespace LAB_03
{
    public class GasContainer : Container, IHazardNotification
    {
        public double Pressure { get; private set; }

        public GasContainer(double cargoMass, string cargoUnit, int height, double tareWeight, int depth, string serialNumber, double maxPayload, double pressure)
            : base(cargoMass, "G" ,height, tareWeight, depth, serialNumber, maxPayload)
        {
            Pressure = pressure;
        }
        
        public override void LoadCargo(double mass)
        {
            double maxAllowedPayload = MaxPayload * 0.95;
            if (mass + CargoMass > maxAllowedPayload)
            {
                Console.WriteLine($"Cannot Load {mass} {CargoUnit} Because It Exceeds The Maximum Allowed Capacity. Current Cargo Mass: {CargoMass} {CargoUnit}, Maximum Allowed Payload: {maxAllowedPayload} {CargoUnit}.");
                throw new OverfillException($"Loading The Cargo Would Exceed The Container's Allowed Payload.");
            }
            CargoMass += mass;
            Console.WriteLine($"Cargo Loaded Successfully. New Cargo Mass: {CargoMass} {CargoUnit}.");
        }
        
        public void NotifyHazard()
        {
            if (Pressure > 10) 
            {
                Console.WriteLine($"Warning: Gas Container {SerialNumber} is under high pressure and may be hazardous.");
            }
        }

        public override string GetContainerType()
        {
            return "Gas";
        }
    }   

}