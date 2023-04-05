using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Commands.UserCommands.PutUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests.PutUser
{
	/// <summary>
	/// Тест для <see cref="PutUserCommandHandler"/>
	/// </summary>
	public class PutUserCommandHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен обновить пользователя, когда команда валидна
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task PutUserCommand_ShouldUpdateUser_WhenCommandValid()
		{
			var command = new PutUserCommand
			{
				Name = "John",
				Surname = "Doe",
				Phone = "+1234567890",
			};
			using var context = CreateInMemoryContext();

			var handler = new PutUserCommandHandler(context, UserService);
			await handler.Handle(command, default);

			var updatedUser = context.Users.FirstOrDefault(x => x.Id == UserService.AdminUser.Id);

			updatedUser.Should().NotBeNull();
			updatedUser!.Name.Should().Be(command.Name);
			updatedUser!.Surname.Should().Be(command.Surname);
			updatedUser!.Phone.Should().Be(command.Phone);
		}
	}
}
