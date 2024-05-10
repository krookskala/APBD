namespace Test_01.Repositories
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
    }
}