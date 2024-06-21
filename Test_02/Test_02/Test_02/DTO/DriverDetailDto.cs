namespace Test_02.DTO
{
    public class DriverDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public int CarNumber { get; set; }
        public string CarManufacturerName { get; set; } = null!;
        public string CarModelName { get; set; } = null!;
    }
}
