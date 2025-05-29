using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityMicroservice.Data;
using IdentityMicroservice.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IdentityMicroservice.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IdentityDbContext _context;
		private readonly IConfiguration _config;

		public AuthController(IdentityDbContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (await _context.Users.AnyAsync(u => u.Username == model.Username))
				return BadRequest("Username already exists");

			var user = new User
			{
				Username = model.Username,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, model.Password),
                Role = model.Role
			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return Ok(new { Message = "User registered successfully" });
		}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null) return Unauthorized("Invalid credentials");

            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }


        [HttpPost("change-password")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
			if (user == null)
				return NotFound("User not found");

			user.PasswordHash = model.NewPassword;
			await _context.SaveChangesAsync();

			return Ok(new { Message = "Password changed successfully" });
		}

		[HttpGet("users")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users.Select(u => new { u.Id, u.Username, u.Role }).ToListAsync();
			return Ok(users);
		}

		[HttpDelete("delete-user")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser([FromBody] DeleteUserModel model)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
			if (user == null)
				return NotFound("User not found");

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return Ok(new { Message = "User deleted successfully" });
		}

		private string GenerateJwtToken(User user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.Role)
			};

			Console.WriteLine($"Generating token for user: {user.Username}, role: {user.Role}");

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}

	public class RegisterModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}

	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}