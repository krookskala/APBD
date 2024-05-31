namespace VeterinaryClinic.DTO
{
    public class VisitDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string AnimalName { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}