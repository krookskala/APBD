namespace Warehouse.ProductWarehouse.Entity
{
    public class ProductWarehouseEntity
    {
        int IdProductWarehouse { get; set; }
        int IdWarehouse { get; set; }
        int IdProduct { get; set; }
        int IdOrder { get; set; }
        int Amount { get; set; }
        double Price { get; set; }
        DateTime CreatedAt { get; set; }
    }
}