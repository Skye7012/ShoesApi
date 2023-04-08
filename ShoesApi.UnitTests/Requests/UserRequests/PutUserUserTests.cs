using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Commands.UserCommands.PutUser;
using ShoesApi.Exceptions;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests
{
	/// <summary>
	/// Тест для <see cref="PutUserCommandHandler"/>
	/// </summary>
	public class PutUserCommandHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен обновить пользователя, когда команда валидна
		/// </summary>
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

		/// <summary>
		/// Должен выкинуть ошибку, когда пользователь не найден
		/// </summary>
		[Fact]
		public async Task PutUserQueryHandler_ShouldThrow_WhenUserNotFound()
		{
			using var context = CreateInMemoryContext(x =>
			{
				x.Users.Remove(UserService.AdminUser);
				x.SaveChanges();
			});

			var handler = new PutUserCommandHandler(context, UserService);
			var handle = async () => await handler.Handle(new PutUserCommand(), default);

			await handle.Should()
				.ThrowAsync<UserNotFoundException>();
		}
	}
}
