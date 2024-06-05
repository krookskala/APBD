using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required][ForeignKey("AnimalType")]
        public int AnimalTypesId { get; set; }

        public AnimalTypes AnimalType { get; set; } = null!;

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}