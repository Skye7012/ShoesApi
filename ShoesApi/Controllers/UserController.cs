using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoesApi.Contracts.Requests.UserRequests.SignInRequest;
using ShoesApi.Contracts.Requests.UserRequests.SignUpRequest;
using ShoesApi.Contracts.Requests.UserRequests.UserGetRequest;
using ShoesApi.Entities;
using ShoesApi.Requests.UserRequests;
using ShoesApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Controller for <see cref="User"/>
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ShoesDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly IUserService _userService;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		/// <param name="configuration">Configuration service</param>
		/// <param name="userService">User service</param>
		public UserController(
			ShoesDbContext context,
			IConfiguration configuration,
			IUserService userService)
		{
			_context = context;
			_configuration = configuration;
			_userService = userService;
		}

		/// <summary>
		/// Get User Credentials
		/// </summary>
		/// <returns>User Credentials</returns>
		[HttpGet]
		[Authorize]
		public async Task<UserGetResponse> Get()
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			return new UserGetResponse()
			{
				Login = login,
				Name = user.Name,
				FirstName = user.FirstName,
				Phone = user.Phone,
			};
		}

		/// <summary>
		/// SignUp
		/// </summary>
		/// <param name="request">Request</param>
		/// <returns>Created userId</returns>
		[HttpPost("SignUp")]
		public async Task<SignUpResponse> SignUp(SignUpRequest request)
		{
			var isLoginUnique = _context.Users.All(x => x.Login != request.Login);
			if (!isLoginUnique)
				throw new Exception("User with such login already exists");

			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			var user = new User();

			user.Login = request.Login;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			user.Name = request.Name;
			user.FirstName = request.FirstName;
			user.Phone = request.Phone;

			await _context.AddAsync(user);
			await _context.SaveChangesAsync();

			return new SignUpResponse()
			{
				UserId = user.Id,
			};
		}

		/// <summary>
		/// SignIn
		/// </summary>
		/// <param name="request">Request</param>
		/// <returns>Authorization token</returns>
		[HttpPost("SignIn")]
		public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == request.Login)
				?? throw new Exception("User not found");

			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				return BadRequest("Wrong password");
			}

			string token = CreateToken(user);

			return Ok(new SignInResponse()
			{
				Token = token,
			});
		}

		/// <summary>
		/// Update User Credentials
		/// </summary>
		/// <param name="request">Request</param>
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
			if (request.FirstName != null)
				user.FirstName = request.FirstName;
			if (request.Phone != null)
				user.Phone = request.Phone;

			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Delete User
		/// </summary>
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

		/// <summary>
		/// CreateToken
		/// </summary>
		/// <param name="user">User</param>
		/// <returns>Authorization token</returns>
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
