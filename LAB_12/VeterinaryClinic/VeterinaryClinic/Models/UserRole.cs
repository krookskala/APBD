using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models;

public class UserRole
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The Name Field Is Required.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Name Should Be Between 8 And 50 Characters!")]
    public required string Name { get; set; }
}