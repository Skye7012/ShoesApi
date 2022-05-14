using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesApi.Entities;
using ShoesApi.Extensions;
using ShoesApi.Requests.UserRequests;
using ShoesApi.Responses.BrandResponses.GetBrandsResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ShoesDbContext _context;
		private readonly IConfiguration _configuration;

		public UserController(
			ShoesDbContext context,
			IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpGet]
		[Authorize]
		public ActionResult<string> Get()
		{
			return Ok("Authirized");
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(RegisterRequest request)
		{
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			var user = new User();

			user.Login = request.Login;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _context.AddAsync(user);
			await _context.SaveChangesAsync();

			return Ok(user.Id);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(LoginRequest request)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == request.Login)
				?? throw new Exception("User not found");

			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				return BadRequest("Wrong password");
			}

			string token = CreateToken(user);

			return Ok(token);
		}

		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Login),
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
	}
}
