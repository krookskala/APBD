using VeterinaryClinic.Models;

namespace VeterinaryClinic.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        Task<bool> ValidateExpiredTokenAsync(string accessToken);
    }
}