using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class AddAnimalDto
    {
        [Required(ErrorMessage = "The Name Field Is Required.")]
        [MaxLength(100, ErrorMessage = "The Name Field Can Contain A Maximum Of 100 Characters.")]
        public required string Name { get; set; } 

        [MaxLength(2000, ErrorMessage = "The Description Field Can Contain A Maximum Of 2000 Characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The AnimalType Field Is Required.")]
        public required string AnimalType { get; set; } 
    }
}