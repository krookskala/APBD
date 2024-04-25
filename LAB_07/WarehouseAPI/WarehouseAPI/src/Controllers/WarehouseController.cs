using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Interfaces;
using WarehouseAPI.Models;
using WarehouseAPI.Models.DTOs;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController (IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(InsertProductDto productWarehouseDto)
        {
            if (productWarehouseDto.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            if (!await _warehouseRepository.DoesProductExists(productWarehouseDto.IdProduct))
            {
                return NotFound($"No product found with ID {productWarehouseDto.IdProduct}.");
            }

            if (!await _warehouseRepository.DoesWarehouseExists(productWarehouseDto.IdWarehouse))
            {
                return NotFound($"No warehouse found with ID {productWarehouseDto.IdWarehouse}.");
            }

            if (!await _warehouseRepository.DoesOrderExists(productWarehouseDto.IdProduct, productWarehouseDto.Amount,
                    productWarehouseDto.CreatedAt))
            {
                return NotFound("No matching order found, or the order was created after the request.");
            }

            var orderId = await _warehouseRepository.GetOrderId(productWarehouseDto.IdProduct);
            if (await _warehouseRepository.WasOrderRealized(orderId))
            {
                return Conflict("This order has already been realized.");
            }

            await _warehouseRepository.UpdateDate(orderId);

            double price = await _warehouseRepository.GetPrice(productWarehouseDto.IdProduct);
            double totalPrice = price * productWarehouseDto.Amount;

            int newId = await _warehouseRepository.InsertIntoProduct_Warehouse(productWarehouseDto, totalPrice,
                orderId);
            return CreatedAtAction(nameof(AddProduct), new { id = newId }, productWarehouseDto);
        }

        [HttpPost("warehouse/procedure")]
        public async Task<IActionResult> AddProductWithProcedure(InsertProductDto productWarehouseDto)
        {
            try
            {
                var newId = await _warehouseRepository.InsertIntoProduct_Warehouse_With_Procedure(productWarehouseDto);
                return Ok(newId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] ProductWarehouseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok();
        }
    }
}