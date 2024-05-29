using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class AnimalDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(2000)] public string? Description { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public required string AnimalType { get; set; }
    }
}