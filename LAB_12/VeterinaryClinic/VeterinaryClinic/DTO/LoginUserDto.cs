using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO;

public class LoginUserDto
{
    [Required]
    public required string Username { get; set; }
        
    [Required]
    public required string Password { get; set; }
}