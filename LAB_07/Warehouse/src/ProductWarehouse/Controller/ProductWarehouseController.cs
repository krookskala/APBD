using Microsoft.AspNetCore.Mvc;
using Warehouse.ProductWarehouse.Controller.Request;
using Warehouse.ProductWarehouse.Interface;

namespace Warehouse.ProductWarehouse.Controller;

[ApiController]
[Route("api/p_warehouses")]
public class ProductWarehouseController : ControllerBase
{
    private readonly IProductWarehouseService _productWarehouseService;
    
    public ProductWarehouseController(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }
    
    [HttpPut]
    public IActionResult CreateProduct(ProductWarehouseRequest productWarehouseRequest)
    {
        _productWarehouseService.CreateProduct(productWarehouseRequest);
        return Created();
    }
}