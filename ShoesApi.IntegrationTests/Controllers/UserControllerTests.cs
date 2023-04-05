using Xunit;
using ShoesApi.Controllers;
using System.Net.Http.Json;
using ShoesApi.CQRS.Commands.UserCommands.SignUpUser;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using ShoesApi.CQRS.Queries.User.GetUser;
using ShoesApi.CQRS.Commands.UserCommands.SignInUser;
using ShoesApi.CQRS.Commands.UserCommands.PutUser;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.IntegrationTests.Controllers
{
	/// <summary>
	/// Тесты для <see cref="UserController"/>
	/// </summary>
	public class UserControllerTests : IntegrationTestsBase
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="factory">Фабрика приложения</param>
		public UserControllerTests(IntegrationTestFactory<Program, ShoesDbContext> factory) : base(factory)
		{
		}

		/// <summary>
		/// Полный happy path
		/// Должен создать пользователя, аутентифицироваться, получить его данные, обновить и удалить
		/// </summary>
		[Fact]
		public async Task UserController_HappyPath_ShouldCreateAndGetAndUpdateAndDeleteUser()
		{
			var userId = await SignUpAsync();
			await SignInAsync();
			await GetAsync();
			await PutAsync(userId);
			await DeleteAsync(userId);
		}

		/// <summary>
		/// Регистрация
		/// </summary>
		/// <returns>Идентификатор пользователя</returns>
		private async Task<int> SignUpAsync()
		{
			var signUpResponse = await Client.PostAsJsonAsync("/User/SignUp", new SignUpUserCommand
			{
				Login = "test",
				Password = "test",
				Name = "test",
				Surname = "test",
				Phone = "88005553535",
			})
			.GetResponseAsyncAs<SignUpUserResponse>();

			signUpResponse.Should().NotBeNull();

			return signUpResponse.UserId;
		}

		/// <summary>
		/// Аутентификация
		/// </summary>
		private async Task SignInAsync()
		{
			var signInResponse = await Client.PostAsJsonAsync("/User/SignIn", new SignInUserCommand
			{
				Login = "test",
				Password = "test",
			})
			.GetResponseAsyncAs<SignInUserResponse>();

			signInResponse.Should().NotBeNull();

			Authenticate(signInResponse.Token);
		}

		/// <summary>
		/// Получение данных о пользователе
		/// </summary>
		private async Task GetAsync()
		{
			var getResponse = await Client.GetAsync("/User").GetResponseAsyncAs<GetUserResponse>();

			getResponse.Should().NotBeNull();

			getResponse!.Login.Should().Be("test");
			getResponse!.Name.Should().Be("test");
			getResponse!.Surname.Should().Be("test");
			getResponse!.Phone.Should().Be("88005553535");
		}

		/// <summary>
		/// Обновление данных о пользователе
		/// </summary>
		private async Task PutAsync(int userId)
		{
			var putResponse = await Client.PutAsJsonAsync("/User", new PutUserCommand
			{
				Name = "test2",
				Surname = "test2",
				Phone = "880055535352",
			});

			putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

			var updatedUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

			updatedUser.Should().NotBeNull();
			updatedUser!.Name.Should().Be("test2");
			updatedUser!.Surname.Should().Be("test2");
			updatedUser!.Phone.Should().Be("880055535352");
		}

		/// <summary>
		/// Удаление пользователя
		/// </summary>
		private async Task DeleteAsync(int userId)
		{
			var deleteResponse = await Client.DeleteAsync("/User");

			deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

			var deletedUser = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

			deletedUser.Should().BeNull();
		}
	}
}
