using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.DTO
{
    public class VisitUpdateDto
    {
        [Required(ErrorMessage = "The Date field is required.")]
        public DateTime Date { get; set; }
    }
}