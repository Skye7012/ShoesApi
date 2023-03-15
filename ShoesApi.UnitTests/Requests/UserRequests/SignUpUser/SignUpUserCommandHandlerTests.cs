using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Commands.UserCommands.SignUpUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests.SignUpUser
{
	/// <summary>
	/// Тест для <see cref="SignUpUserCommandHandler"/>
	/// </summary>
	public class SignUpUserCommandHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен создать пользователя, когда команда валидна
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task SignUpUserCommand_ShouldCreateUser_WhenCommandValid()
		{
			var signUpCommand = new SignUpUserCommand
			{
				Login = "testlogin",
				Password = "testpassword",
				Name = "TestName",
				Surname = "TestSurname",
				Phone = "1234567890"
			};
			using var context = CreateInMemoryContext();

			var handler = new SignUpUserCommandHandler(context, UserService);
			var response = await handler.Handle(signUpCommand, default);

			var createdUser = context.Users.FirstOrDefault(x => x.Id == response.UserId);

			createdUser.Should().NotBeNull();
			createdUser!.Name.Should().Be(signUpCommand.Name);
			createdUser!.Surname.Should().Be(signUpCommand.Surname);
			createdUser!.Phone.Should().Be(signUpCommand.Phone);
			createdUser!.Login.Should().Be(signUpCommand.Login);
		}
	}
}
