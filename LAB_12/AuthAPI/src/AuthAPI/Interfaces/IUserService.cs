namespace AuthAPI
{
    public class IUserService
    {
        Task<AuthDto> RegisterUserAsync(CreateUserDto createUserDto);
        Task<AuthDto> AuthenticateUserAsync(LoginDto loginDto);
        Task<AuthDto> RefreshTokenAsync(string refreshToken);
    }
}