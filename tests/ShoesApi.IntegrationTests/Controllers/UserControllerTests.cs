using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Api.Controllers;
using ShoesApi.Contracts.Requests.Users.GetUser;
using ShoesApi.Contracts.Requests.Users.PutUser;
using ShoesApi.Contracts.Requests.Users.SignInUser;
using ShoesApi.Contracts.Requests.Users.SignUpUser;
using ShoesApi.Infrastructure.Persistence;
using Xunit;

namespace ShoesApi.IntegrationTests.Controllers;

/// <summary>
/// Тесты для <see cref="UserController"/>
/// </summary>
public class UserControllerTests : IntegrationTestsBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="factory">Фабрика приложения</param>
	public UserControllerTests(IntegrationTestFactory<Program, ApplicationDbContext> factory) : base(factory)
	{
	}

	/// <summary>
	/// Должен вернуть данные о пользователе, когда он существует
	/// </summary>
	[Fact]
	public async Task GetAsync_ShouldReturnUser_WhenUserExist()
	{
		Authenticate();
		var getResponse = await Client.GetAsync("/User").GetResponseAsyncAs<GetUserResponse>();

		getResponse.Should().NotBeNull();

		getResponse!.Login.Should().Be(Seeder.AdminUser.Login);
		getResponse!.Name.Should().Be(Seeder.AdminUser.Name);
		getResponse!.Surname.Should().Be(Seeder.AdminUser.Surname);
		getResponse!.Phone.Should().Be(Seeder.AdminUser.Phone);
	}

	/// <summary>
	/// Должен создать пользователя, когда запрос валиден
	/// </summary>
	[Fact]
	public async Task SignUpAsync_ShouldCreateUser_WhenRequestValid()
	{
		var signUpResponse = await Client
			.PostAsJsonAsync("/User/SignUp", new SignUpUserRequest
			{
				Login = "Login",
				Password = "Password",
				Name = "Name",
				Surname = "Surname",
				Phone = "88005553535",
			})
			.GetResponseAsyncAs<SignUpUserResponse>();

		signUpResponse.Should().NotBeNull();

		var createdUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == signUpResponse.UserId);

		createdUser.Should().NotBeNull();
		createdUser!.Login.Should().Be("Login");
		createdUser!.Name.Should().Be("Name");
		createdUser!.Surname.Should().Be("Surname");
		createdUser!.Phone.Should().Be("88005553535");

		UserService.VerifyPasswordHash("Password", createdUser.PasswordHash, createdUser.PasswordSalt)
			.Should().BeTrue();
	}

	/// <summary>
	/// Должен создать правильный токен, когда запрос валиден
	/// </summary>
	[Fact]
	public async Task SignInAsync_ShouldCreateValidToken_WhenRequestValid()
	{
		var signInResponse = await Client
			.PostAsJsonAsync("/User/SignIn", new SignInUserRequest
			{
				Login = Seeder.AdminUser.Login,
				Password = "AdminPassword",
			})
			.GetResponseAsyncAs<SignInUserResponse>();

		signInResponse.Should().NotBeNull();
		signInResponse.Token.Should().NotBeNullOrWhiteSpace();

		var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(signInResponse.Token);

		var nameClaimValue = decodedToken.Claims
			.First(x => x.Type == ClaimTypes.Name)
			.Value;

		nameClaimValue.Should().Be(Seeder.AdminUser.Login);

		Authenticate(signInResponse.Token);
	}

	/// <summary>
	/// Должен обновить данные о пользователе, когда запрос валиден
	/// </summary>
	[Fact]
	public async Task PutAsync_ShouldUpdateUser_WhenRequestValid()
	{
		Authenticate();
		var putResponse = await Client.PutAsJsonAsync("/User", new PutUserRequest
		{
			Name = "Name2",
			Surname = "Surname2",
			Phone = "880055535352",
		});

		putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

		DbContext.Instance.ChangeTracker.Clear();
		var updatedUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == Seeder.AdminUser.Id);

		updatedUser.Should().NotBeNull();
		updatedUser!.Name.Should().Be("Name2");
		updatedUser!.Surname.Should().Be("Surname2");
		updatedUser!.Phone.Should().Be("880055535352");
	}

	// <summary>
	/// Должен удалить пользователя, когда он существует
	/// </summary>
	[Fact]
	public async Task DeleteAsync_ShouldDeleteUser_WhenHeExists()
	{
		Authenticate();
		var deleteResponse = await Client.DeleteAsync("/User");

		deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

		var deletedUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == Seeder.AdminUser.Id);

		deletedUser.Should().BeNull();
	}
}
