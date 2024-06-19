using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.DTO;
using Test.Helpers;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Supplier)
                .Select(o => new 
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                    Items = o.OrderItems.Select(oi => new 
                    {
                        ProductName = oi.Product.ProductName,
                        SupplierName = oi.Product.Supplier.CompanyName,
                        UnitPrice = oi.UnitPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Supplier)
                .Select(o => new 
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                    Items = o.OrderItems.Select(oi => new 
                    {
                        ProductName = oi.Product.ProductName,
                        SupplierName = oi.Product.Supplier.CompanyName,
                        UnitPrice = oi.UnitPrice,
                        Quantity = oi.Quantity
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] OrderCreationDto orderDto, CancellationToken cancellationToken)
        {
            if (orderDto.Items == null || !orderDto.Items.Any())
                throw new BadRequestException("Order Must Include At Least One Item!");
            
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.FirstName == orderDto.FirstName && c.LastName == orderDto.LastName, cancellationToken);

            if (customer == null)
            {
                customer = new Customer { FirstName = orderDto.FirstName, LastName = orderDto.LastName };
                _context.Customers.Add(customer);
            }
            
            var productIds = orderDto.Items.Select(i => i.ProductId).Distinct();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                OrderItems = orderDto.Items.Select(i => 
                {
                    if (!products.TryGetValue(i.ProductId, out var product))
                        throw new NotFoundException($"Product with ID {i.ProductId} not found.");

                    return new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = _context.Products.Where(p => p.Id == i.ProductId).Select(p => p.UnitPrice)
                            .FirstOrDefault()
                    };
                }).ToList()
            };

            order.TotalAmount = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);
            _context.Orders.Add(order);

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);;
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
