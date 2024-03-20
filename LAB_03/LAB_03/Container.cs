namespace LAB_03
{
    public abstract class Container
    {
        public double CargoMass { get; protected set; }
        public int Height { get; protected set; }
        public double TareWeight { get; protected set; }
        public int Depth { get; protected set; }
        public string SerialNumber { get; protected set; }
        public double MaxPayload { get; protected set; }

        protected Container(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload)
        {
            if (cargoMass < 0 || height < 0 || tareWeight < 0 || depth < 0 || maxPayload < 0)
            {
                throw new ArgumentException("Negative Values Are Not Allowed For Container Properties.");
            }
    
            CargoMass = cargoMass;
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            SerialNumber = serialNumber;
            MaxPayload = maxPayload;
        }
        public virtual string GetContainerType()
        {
            return this.GetType().Name;
        }
        public abstract void LoadCargo(double mass);
        public abstract void EmptyCargo();
    }
}