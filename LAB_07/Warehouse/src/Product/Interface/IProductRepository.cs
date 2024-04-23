using Warehouse.Product.Entity;

namespace Warehouse.Product.Interface
{
    public interface IProductRepository
    {
        ProductEntity? Get(int id);
    }
}