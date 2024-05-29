namespace VeterinaryClinic.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] RowVersion { get; set; } = null!;
    }
}