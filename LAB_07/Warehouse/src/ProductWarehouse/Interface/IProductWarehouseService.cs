using Warehouse.ProductWarehouse.Controller.Request;
using Warehouse.ProductWarehouse.Controller.Response;

namespace Warehouse.ProductWarehouse.Interface
{
    public interface IProductWarehouseService
    {
        ProductWarehouseResponse CreateProduct(ProductWarehouseRequest productWarehouseRequest);
    }
}