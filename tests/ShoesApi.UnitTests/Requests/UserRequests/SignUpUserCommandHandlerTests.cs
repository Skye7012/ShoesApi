using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Users.Commands.SignUpUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests;

/// <summary>
/// Тест для <see cref="SignUpUserCommandHandler"/>
/// </summary>
public class SignUpUserCommandHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен создать пользователя, когда команда валидна
	/// </summary>
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

	/// <summary>
	/// Должен выкинуть ошибку, когда указан неуникальный логин
	/// </summary>
	[Fact]
	public async Task SignUpUserQueryHandler_ShouldThrow_WhenLoginIsNotUnique()
	{
		var signUpCommand = new SignUpUserCommand
		{
			Login = UserService.GetLogin(),
			Password = "testpassword",
			Name = "TestName",
			Surname = "TestSurname",
			Phone = "1234567890"
		};

		using var context = CreateInMemoryContext();

		var handler = new SignUpUserCommandHandler(context, UserService);
		var handle = async () => await handler.Handle(signUpCommand, default);

		await handle.Should()
			.ThrowAsync<ValidationException>()
			.WithMessage("Пользователь с таким логином уже существует");
	}
}
