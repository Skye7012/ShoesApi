using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Users.Queries.GetUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests;

/// <summary>
/// Тест для <see cref="GetUserQueryHandler"/>
/// </summary>
public class GetUserQueryHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен информацию о пользователе, если он существует
	/// </summary>
	[Fact]
	public async Task GetUserQueryHandler_ShouldReturnUserInfo_IfHeExists()
	{
		using var context = CreateInMemoryContext();
		var handler = new GetUserQueryHandler(context, UserService);
		var result = await handler.Handle(new GetUserQuery(), default);

		result.Should().NotBeNull();

		result.Name.Should().Be(UserService.AdminUser.Name);
		result.Login.Should().Be(UserService.AdminUser.Login);
		result.Surname.Should().Be(UserService.AdminUser.Surname);
		result.Phone.Should().Be(UserService.AdminUser.Phone);
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда пользователь не найден
	/// </summary>
	[Fact]
	public async Task GetUserQueryHandler_ShouldThrow_WhenUserNotFound()
	{
		using var context = CreateInMemoryContext(x =>
		{
			x.Users.Remove(UserService.AdminUser);
			x.SaveChanges();
		});

		var handler = new GetUserQueryHandler(context, UserService);
		var handle = async () => await handler.Handle(new GetUserQuery(), default);

		await handle.Should()
			.ThrowAsync<UserNotFoundException>();
	}
}
