using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Email { get; set; } = null!;

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}