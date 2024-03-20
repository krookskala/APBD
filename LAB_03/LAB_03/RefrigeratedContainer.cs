namespace LAB_03
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; set; }
        public double Temperature { get; set; }

        public RefrigeratedContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, string productType, double temperature)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            ProductType = productType;
            Temperature = temperature;
        }

        public override void LoadCargo(double mass)
        {
            if (mass > MaxPayload)
            {
                throw new OverfillException($"Attempting To Overfill Refrigerated Container {SerialNumber}.");
            }

            if (ProductTemperatureRequirements.Requirements.TryGetValue(ProductType, out var requiredTemp))
            {
                if (Temperature < requiredTemp.Min || Temperature > requiredTemp.Max)
                {
                    throw new ArgumentException($"{ProductType} Requires A Temperature Of {requiredTemp.Min} Degrees Celsius.");
                }
            }
            else
            {
                throw new ArgumentException($"No Temperature Requirements Defined For {ProductType}.");
            }
            CargoMass = mass;
        }

        public override void EmptyCargo()
        {
            CargoMass = 0;
        }
        
        public static class ProductTemperatureRequirements
        {
            public static readonly Dictionary<string, (double Min, double Max)> Requirements = new Dictionary<string, (double Min, double Max)>
            {
                {"Bananas", (13.3, 13.3)},
                {"Chocolate", (18, 18)},
                {"Fish", (2, 2)},
                {"Meat", (-15, -15)},
                {"Ice Cream", (-18, -18)},
                {"Frozen Pizza", (-30, -30)},
                {"Cheese", (7.2, 7.2)},
                {"Sausages", (5, 5)},
                {"Butter", (20.5, 20.5)},
                {"Eggs", (19, 19)}
            };
        }
    }
}