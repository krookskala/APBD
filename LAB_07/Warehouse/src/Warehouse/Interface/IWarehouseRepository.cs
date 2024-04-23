using Warehouse.Warehouse.Entity;

namespace Warehouse.Warehouse.Interface
{
    public interface IWarehouseRepository
    {
        WarehouseEntity Get(long idWarehouse);
    }
}