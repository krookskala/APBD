namespace LAB_03
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxPayload, string productType, double temperature)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxPayload)
        {
            ProductType = productType;
            Temperature = temperature;
        }

        public override string GetContainerType()
        {
            return "Refrigerated";
        }
    }
}