using Microsoft.Build.Framework;

namespace VeterinaryClinic.DTO
{
    public class VisitUpdateDto
    {
        [Required]
        public DateTime Date { get; set; }
    }
}