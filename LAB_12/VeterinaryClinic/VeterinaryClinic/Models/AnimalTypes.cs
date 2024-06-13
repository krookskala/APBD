using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models
{
    public class AnimalTypes
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; } = null!;
        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}