namespace LAB_03
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double cargoMass, string cargoUnit, int height, double tareWeight, int depth, string serialNumber,
            double maxPayload, string productType, double temperature)
            : base(cargoMass, "Kg", height, tareWeight, depth, serialNumber, maxPayload)
        {
            if (!ProductTemperature.IsValidProduct(productType))
            {
                throw new ArgumentException($"Unknown Product Type: {productType}. Please Enter A Product From The List.");
            }

            var (Min, Max) = ProductTemperature.GetTemperatureRange(productType);
            if (temperature < Min || temperature > Max)
            {
                throw new ArgumentException(
                    $"The Temperature For {productType} Must Be Between {Min} And {Max} Degrees Celsius.");
            }

            ProductType = productType;
            Temperature = temperature;
        }

        public override void LoadCargo(double mass)
        {
            if (mass + CargoMass > MaxPayload)
            {
                throw new OverfillException("Loading The Cargo Would Exceed The Container's Maximum Payload.");
            }

            CargoMass += mass;
        }
        
        public override string GetContainerType()
        {
            return "Refrigerated";
        }
    }
}