using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesApi.Entities;
using ShoesApi.Extensions;
using ShoesApi.Requests.UserRequests;
using ShoesApi.Responses.BrandResponses.GetBrandsResponse;
using ShoesApi.Services;
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
		private readonly IUserService _userService;

		public UserController(
			ShoesDbContext context,
			IConfiguration configuration,
			IUserService userService)
		{
			_context = context;
			_configuration = configuration;
			_userService = userService;
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
			var isLoginUnique = _context.Users.All(x => x.Login != request.Login);
			if (!isLoginUnique)
				throw new Exception("User with such login already existis");

			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			var user = new User();

			user.Login = request.Login;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			user.Name = request.Name;
			user.Fname = request.Fname;
			user.Phone = request.Phone;

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

		[HttpPut]
		[Authorize]
		public async Task Put(UserPutRequest request)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			if (request.Name != null)
				user.Name = request.Name;
			if (request.Fname != null)
				user.Fname = request.Fname;
			if (request.Phone != null)
				user.Phone = request.Phone;

			await _context.SaveChangesAsync();
		}

		[HttpDelete]
		[Authorize]
		public async Task Delete()
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}

		#region token and pass

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

		#endregion
	}
}
