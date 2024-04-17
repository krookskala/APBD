using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinicApi.Models.DTO;

public class AddAnimal
{
    
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string? Category { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Area { get; set; }
}