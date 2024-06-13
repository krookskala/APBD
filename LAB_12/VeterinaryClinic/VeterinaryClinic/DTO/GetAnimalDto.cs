using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class GetAnimalDto
    {
        [Required(ErrorMessage = "The AnimalId Field Is Required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name Field Is Required.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "The AnimalType Field Is Required.")]
        public string AnimalType { get; set; } = null!;
    }
}