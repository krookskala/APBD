using Microsoft.Build.Framework;

namespace VeterinaryClinic.DTO
{
    public class VisitCreateDto
    {
        [Required] 
        public int EmployeeId { get; set; }

        [Required] 
        public int AnimalId { get; set; }

        [Required] 
        public DateTime Date { get; set; }
    }
}