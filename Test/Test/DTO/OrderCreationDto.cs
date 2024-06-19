namespace Test.DTO;

public class OrderCreationDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public List<ProductDto> Items { get; set; } = null!;
}