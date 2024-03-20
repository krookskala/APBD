namespace LAB_03
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; set; } 
        public GasContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, double pressure)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            Pressure = pressure;
        }

        public override void LoadCargo(double mass)
        {
            if (mass > MaxPayload)
            {
                throw new OverfillException($"Attempting To Overfill Gas Container {SerialNumber}.");
            }
            CargoMass = mass;
        }

        public override void EmptyCargo()
        {
            CargoMass *= 0.05;
        }

        public void NotifyHazard()
        {
            Console.WriteLine($"Hazardous Event Notified For Gas Container {SerialNumber}.");
        }
    }
}