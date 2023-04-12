using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Users.Commands.SignInUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests;

/// <summary>
/// Тест для <see cref="SignInUserCommandHandler"/>
/// </summary>
public class SignInUserCommandHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен создать пользователя, когда команда валидна
	/// </summary>
	[Fact]
	public async Task SignInUserCommand_ShouldCreateUser_WhenCommandValid()
	{
		var signInCommand = new SignInUserCommand
		{
			Login = UserService.AdminUser.Login,
			Password = UserService.AdminUserPassword,
		};

		using var context = CreateInMemoryContext();

		var handler = new SignInUserCommandHandler(context, UserService);
		var response = await handler.Handle(signInCommand, default);

		response.Token.Should().NotBeNullOrWhiteSpace();
		response.Token.Should().Be(UserService.CreateToken(UserService.AdminUser));
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда пользователь не найден
	/// </summary>
	[Fact]
	public async Task SignInUserQueryHandler_ShouldThrow_WhenUserNotFound()
	{
		var signInCommand = new SignInUserCommand
		{
			Login = UserService.AdminUser.Login,
			Password = UserService.AdminUserPassword,
		};

		using var context = CreateInMemoryContext(x =>
		{
			x.Users.Remove(UserService.AdminUser);
			x.SaveChanges();
		});

		var handler = new SignInUserCommandHandler(context, UserService);
		var handle = async () => await handler.Handle(signInCommand, default);

		await handle.Should()
			.ThrowAsync<UserNotFoundException>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда указан неправильный пароль
	/// </summary>
	[Fact]
	public async Task SignInUserQueryHandler_ShouldThrow_WhenPasswordIsWrong()
	{
		var signInCommand = new SignInUserCommand
		{
			Login = UserService.AdminUser.Login,
			Password = UserService.AdminUserPassword + "Wrong",
		};

		using var context = CreateInMemoryContext();

		var handler = new SignInUserCommandHandler(context, UserService);
		var handle = async () => await handler.Handle(signInCommand, default);

		await handle.Should()
			.ThrowAsync<ValidationException>()
			.WithMessage("Неправильный пароль");
	}
}
