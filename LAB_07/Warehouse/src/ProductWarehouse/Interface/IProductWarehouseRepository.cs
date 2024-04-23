using Warehouse.ProductWarehouse.Entity;

namespace Warehouse.ProductWarehouse.Interface
{
    public interface IProductWarehouseRepository
    {
        ProductWarehouseEntity SaveProduct(ProductWarehouseEntity productWarehouseEntity);
    }
}