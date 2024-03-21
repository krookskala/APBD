namespace LAB_03;

public class ProductTemperature
{
    public static readonly Dictionary<string, (double Min, double Max)> ProductTemperatures = new Dictionary<string, (double Min, double Max)>(StringComparer.OrdinalIgnoreCase)
    {
        { "Bananas", (13.3, 20.0) },
        { "Chocolate", (18, 23) },
        { "Fish", (2, 7) },
        { "Meat", (-15, -10) },
        { "Ice Cream", (-18, -13) },
        { "Frozen Pizza", (-30, -25) },
        { "Cheese", (7.2 , 12.2) },
        { "Sausages", (5 , 10) },
        { "Butter", (20.5 , 25.5) },
        { "Eggs", (19 , 24) }
    };
    
        
    public static bool IsValidProduct(string productType)
    {
        return ProductTemperatures.ContainsKey(productType);
    }
    
    public static (double Min, double Max) GetTemperatureRange(string productType)
    {
        if(ProductTemperatures.TryGetValue(productType, out var range))
        {
            return range;
        }
        return (0, 0);
    }
}
