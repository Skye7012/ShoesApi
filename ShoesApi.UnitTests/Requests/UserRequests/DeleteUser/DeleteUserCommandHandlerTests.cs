using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Commands.UserCommands.DeleteUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests.DeleteUser
{
	/// <summary>
	/// Тест для <see cref="DeleteUserCommandHandler"/>
	/// </summary>
	public class DeleteUserCommandHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен удалить пользователя, когда команда валидна
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task DeleteUserCommand_ShouldCreateUser_WhenCommandValid()
		{
			using var context = CreateInMemoryContext();

			var handler = new DeleteUserCommandHandler(context, UserService);
			await handler.Handle(new DeleteUserCommand(), default);

			var adminUser = context.Users
				.FirstOrDefault(x => x.Id == UserService.AdminUser.Id);

			adminUser.Should().BeNull();
		}
	}
}
