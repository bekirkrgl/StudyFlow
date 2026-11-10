
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyFlow.Api.Data;
using StudyFlow.Api.Models;

namespace StudyFlow.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly StudyFlowDbContext _db;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IConfiguration _config;

        public AuthController(
            StudyFlowDbContext db,
            IPasswordHasher<User> hasher,
            IConfiguration config)
        {
            _db = db;
            _hasher = hasher;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequest request)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            if (await _db.Users.AnyAsync(u => u.Email == email))
            {
                return BadRequest("Bu e-posta zaten kayitli.");
            }

            var role = await _db.Roles
                .FirstOrDefaultAsync(r => r.Name == "Student");

            if (role == null)
            {
                return StatusCode(500, "Student rolu bulunamadi.");
            }

            var user = new User
            {
                FullName = request.FullName.Trim(),
                Email = email,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash =
                _hasher.HashPassword(user, request.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                Message = "Kayit basarili.",
                user.Id,
                user.FullName,
                user.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return Unauthorized("E-posta veya sifre hatali.");
            }

            var result = _hasher.VerifyHashedPassword(
                user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("E-posta veya sifre hatali.");
            }

            if (result ==
                PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash =
                    _hasher.HashPassword(user, request.Password);

                await _db.SaveChangesAsync();
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: "StudyFlow.Api",
                audience: "StudyFlow.Mobile",
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),

                ExpiresAt = expiresAt,
                user.Id,
                user.FullName,
                user.Email,
                Role = user.Role.Name
            });
        }

        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier),

                Email = User.FindFirstValue(
                    ClaimTypes.Email),

                Role = User.FindFirstValue(
                    ClaimTypes.Role)
            });
        }
    }

    public class RegisterRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
