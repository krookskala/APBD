using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Visit
    {
        public int Id { get; set; }

        [Required][ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        [Required][ForeignKey("Animal")]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}