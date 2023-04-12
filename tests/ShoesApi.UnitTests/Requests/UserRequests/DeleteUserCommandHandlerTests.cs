using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Users.Commands.DeleteUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests;

/// <summary>
/// Тест для <see cref="DeleteUserCommandHandler"/>
/// </summary>
public class DeleteUserCommandHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен удалить пользователя, когда команда валидна
	/// </summary>
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

	/// <summary>
	/// Должен выкинуть ошибку, когда пользователь не найден
	/// </summary>
	[Fact]
	public async Task DeleteUserCommand_ShouldThrow_WhenUserNotFound()
	{
		using var context = CreateInMemoryContext(x =>
		{
			x.Users.Remove(UserService.AdminUser);
			x.SaveChanges();
		});

		var handler = new DeleteUserCommandHandler(context, UserService);
		var handle = async () => await handler.Handle(new DeleteUserCommand(), default);

		await handle.Should()
			.ThrowAsync<UserNotFoundException>();
	}
}
