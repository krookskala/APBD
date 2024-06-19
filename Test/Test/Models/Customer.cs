namespace Test.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = null!;
}