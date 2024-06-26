using System.ComponentModel.DataAnnotations;

namespace Test_02.DTO;

public class DriverCreateDto
{
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateTime Birthday { get; set; }

    [Required]
    public int CarId { get; set; }
}