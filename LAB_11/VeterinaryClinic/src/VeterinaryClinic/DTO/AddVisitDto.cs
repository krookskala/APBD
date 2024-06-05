using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class AddVisitDto
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; } = null!;

        [Required]
        public string AnimalName { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}