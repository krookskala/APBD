using System.ComponentModel.DataAnnotations;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.DTO;

public class UserDto
{
    [Required(ErrorMessage = "The Username Is Required.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Username Should Be Between 8 And 50 Characters!")]
    public required string Username { get; set; }
        
    [Required(ErrorMessage = "The Password Is Required.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Should Be Between 8 And 50 Characters!")]
    public required string Password { get; set; }

    public User Map()
    {
        return new User
        {
            Username = Username,
            Password = Password
        };
    }
}