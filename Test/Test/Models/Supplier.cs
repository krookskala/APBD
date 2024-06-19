namespace Test.Models;

public class Supplier
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Fax { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
}