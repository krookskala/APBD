namespace LAB_03
{
    public abstract class Container
    {
        public double CargoMass { get; protected set; }
        public int Height { get; private set; }
        public double TareWeight { get; private set; }
        public int Depth { get; private set; }
        public string SerialNumber { get; private set; }
        public double MaxPayload { get; private set; }

        protected Container(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload)
        {
            CargoMass = cargoMass;
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            SerialNumber = serialNumber;
            MaxPayload = maxPayload;
        }

        public abstract string GetContainerType();
        public virtual void LoadCargo(double mass){}

    }
}