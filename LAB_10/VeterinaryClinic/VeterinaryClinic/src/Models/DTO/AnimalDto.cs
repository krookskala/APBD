using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models.DTO
{
    public class AnimalDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public required string Name { get; set; }
        
        [MinLength(0)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public required string Category { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public required string Area { get; set; }
    }
}