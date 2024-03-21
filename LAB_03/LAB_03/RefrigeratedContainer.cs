namespace LAB_03
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber,
            double maxPayload, string productType, double temperature)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            if (!ProductTemperature.ProductTemperatures.ContainsKey(productType))
            {
                throw new ArgumentException($"Unknown Product Type: {productType}.");
            }

            double requiredTemperature = ProductTemperature.ProductTemperatures[productType];
            if (temperature < requiredTemperature)
            {
                throw new ArgumentException(
                    $"The Temperature For {productType} Cannot Be Lower Than {requiredTemperature}.");
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

        public void AdjustTemperature(double newTemperature)
        {
            double requiredTemperature = ProductTemperature.ProductTemperatures[ProductType];
            if (newTemperature < requiredTemperature)
            {
                Console.WriteLine(
                    $"Warning: The New Temperature {newTemperature}°C Is Too Low For {ProductType}, Which Requires At Least {requiredTemperature}°C.");
            }
            else
            {
                Temperature = newTemperature;
                Console.WriteLine($"Temperature Adjusted To {Temperature}°C For {ProductType}.");
            }
        }

        public void CheckContainerState()
        {
            Console.WriteLine(
                $"Container {SerialNumber} Storing {ProductType} At {Temperature}°C With {CargoMass}/{MaxPayload}kg Loaded.");
            if (Temperature < ProductTemperature.ProductTemperatures[ProductType])
            {
                Console.WriteLine(
                    $"Warning: Temperature For {ProductType} Is Below Required {ProductTemperature.ProductTemperatures[ProductType]}°C.");
            }
        }
    }
}