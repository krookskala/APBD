namespace VeterinaryClinic.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int AnimalId { get; set; }
        public Animal Animal { get; set; } = null!;
        public DateTime Date { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}