using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Commands.UserCommands.SignInUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests.SignInUser
{
	/// <summary>
	/// Тест для <see cref="SignInUserCommandHandler"/>
	/// </summary>
	public class SignInUserCommandHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен создать пользователя, когда команда валидна
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task SignInUserCommand_ShouldCreateUser_WhenCommandValid()
		{
			var SignInCommand = new SignInUserCommand
			{
				Login = UserService.AdminUser.Login,
				Password = "jKw7Oae8Tb0f3sYp",
			};

			using var context = CreateInMemoryContext();

			var handler = new SignInUserCommandHandler(context, UserService);
			var response = await handler.Handle(SignInCommand, default);

			response.Token.Should().NotBeNullOrWhiteSpace();
			response.Token.Should().Be(UserService.CreateToken(UserService.AdminUser));
		}
	}
}
