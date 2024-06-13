namespace VeterinaryClinic.DTO
{
    public class LoginDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}