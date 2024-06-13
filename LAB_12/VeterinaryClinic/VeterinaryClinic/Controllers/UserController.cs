using VeterinaryClinic.DTO;
using VeterinaryClinic.Interfaces;
using VeterinaryClinic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AnimalContext _context;
        private readonly IAuthenticationService _authService;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserController(AnimalContext context, IAuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }

        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.Name == "User", cancellationToken);

            if (userRole == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "UserRole Not Found",
                    Detail = "UserRole Does Not Exist In The Database!",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username, cancellationToken);

            if (existingUser != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Username Exists",
                    Detail = "Username Already Exists In The Database!",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            var user = userDto.Map();
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            user.RoleId = userRole.Id;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Created("", null);
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginDto>> LoginUser([FromBody] LoginUserDto loginUser, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == loginUser.Username, cancellationToken);

            if (user == null)
            {
                return Unauthorized();
            }

            var verificationRes = _passwordHasher.VerifyHashedPassword(user, user.Password, loginUser.Password);

            if (verificationRes == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var responseDto = new LoginDto
            {
                AccessToken = _authService.GenerateAccessToken(user),
                RefreshToken = _authService.GenerateRefreshToken()
            };

            user.RefreshToken = responseDto.RefreshToken;
            user.RefreshTokenExpire = DateTime.Now.AddMinutes(60);

            _context.Entry(user).State = EntityState.Modified;
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(responseDto);
        }

        [Route("refresh")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginDto>> RefreshToken([FromBody] LoginDto auth, CancellationToken cancellationToken)
        {
            var isValid = await _authService.ValidateExpiredTokenAsync(auth.AccessToken);

            if (!isValid)
            {
                return Forbid();
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.RefreshToken == auth.RefreshToken, cancellationToken);

            if (user == null || user.RefreshTokenExpire < DateTime.Now)
            {
                return Forbid();
            }

            var responseDto = new LoginDto
            {
                AccessToken = _authService.GenerateAccessToken(user),
                RefreshToken = _authService.GenerateRefreshToken()
            };

            user.RefreshToken = responseDto.RefreshToken;
            user.RefreshTokenExpire = DateTime.Now.AddMinutes(30);

            _context.Entry(user).State = EntityState.Modified;
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(responseDto);
        }
    }
}
