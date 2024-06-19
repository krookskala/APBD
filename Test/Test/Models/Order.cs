using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = null!;
    
    [ConcurrencyCheck]
    public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString(); 
}