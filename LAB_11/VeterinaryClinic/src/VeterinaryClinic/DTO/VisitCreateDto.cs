using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class VisitCreateDto
    {
        [Required(ErrorMessage = "The EmployeeId Field Is Required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "The AnimalId Field Is Required.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "The Date Field Is Required.")]
        public DateTime Date { get; set; }
    }
}