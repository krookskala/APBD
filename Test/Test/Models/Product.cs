namespace Test.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public int SupplierId { get; set; }
    public decimal UnitPrice { get; set; }
    public string Package { get; set; } = null!;
    public bool IsDiscontinued { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = null!;
}