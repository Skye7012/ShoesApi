using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using ShoesApi.Domain.Entities;
using ShoesApi.Infrastructure.Services;
using Xunit;

namespace ShoesApi.UnitTests.Services;

/// <summary>
/// Тесты для <see cref="UserService"/>
/// </summary>
public class UserServiceTests : UnitTestBase
{
	private readonly UserService _sut;
	private readonly IConfiguration _configuration;

	/// <inheritdoc/>
	public UserServiceTests()
	{
		var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
		_configuration = Substitute.For<IConfiguration>();

		_configuration
			.GetSection("AppSettings:Token").Value
			.Returns(x => "jKw7Oae8Tb0f3sYp");

		_sut = new UserService(httpContextAccessor, _configuration);
	}

	/// <summary>
	/// Созданный пароль должен верифицироваться, а неверный пароль нет
	/// </summary>
	[Fact]
	public void VerifyPasswordHash_ShoudCreateValidHashAndSalt()
	{
		var password = "jKw7Oae8Tb0f3sYp";

		_sut.CreatePasswordHash(
			password,
			out var hash,
			out var salt);

		_sut.VerifyPasswordHash(password, hash, salt)
			.Should().BeTrue();

		_sut.VerifyPasswordHash(password, salt, hash)
			.Should().BeFalse();
	}

	/// <summary>
	/// Должен создать правильный токен с правильными клеймами
	/// </summary>
	[Fact]
	public void CreateToken_ShoudCreateValidJwtToken()
	{
		var user = new User()
		{
			Login = "username",
			PasswordHash = new byte[] { 0x12, 0x34, 0x56 },
			PasswordSalt = new byte[] { 0xAB, 0xCD, 0xEF },
			Name = "John",
			Surname = "Doe",
			Phone = "123-45-67"
		};

		var token = _sut.CreateToken(user);

		var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

		var nameClaimValue = decodedToken.Claims
			.First(x => x.Type == ClaimTypes.Name)
			.Value;

		nameClaimValue.Should().Be(user.Login);

		_configuration
			.Received()
				.GetSection("AppSettings:Token");
	}
}
