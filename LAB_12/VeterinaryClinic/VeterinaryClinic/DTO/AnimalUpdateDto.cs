using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class AnimalUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}