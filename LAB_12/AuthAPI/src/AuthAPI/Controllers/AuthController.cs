using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.DTO;
using Tutorial12.API.Entities;
using Tutorial12.API.Services;

namespace Tutorial12.API.Controllers
{
    [Route("api/auth")]
    [ApiController] 
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IAuthenticationService _authService;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthController(ApplicationContext context, IAuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUser, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User", cancellationToken);
            if (role == null)
            {
                return BadRequest("Default User Role Not Found.");
            }

            var user = createUser.Map();
            user.Password = _passwordHasher.HashPassword(user, createUser.Password);
            user.RoleId = role.Id;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthDto>> LoginUser([FromBody] LoginUserDto loginUser, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginUser.Username, cancellationToken);

            if (user == null)
            {
                ModelState.AddModelError("Username", "Username Does Not Exist.");
                return Unauthorized(ModelState);
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginUser.Password);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("Password", "Incorrect Password.");
                return Unauthorized(ModelState);
            }

            var tokens = _authService.GenerateTokens(user);
            return Ok(tokens);
        }


        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AuthDto>> RefreshToken([FromBody] TokenRequestDto tokenRequest, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == tokenRequest.RefreshToken && u.RefreshTokenExpire > DateTime.Now, cancellationToken);

            if (user == null)
            {
                return Forbid("Refresh token is invalid or expired.");
            }

            var newTokens = _authService.GenerateTokens(user);
            user.RefreshToken = newTokens.RefreshToken;
            user.RefreshTokenExpire = DateTime.Now.AddDays(1);  

            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(newTokens);
        }
    }
}
