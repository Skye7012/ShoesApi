using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Queries.User.GetUser;
using Xunit;

namespace ShoesApi.UnitTests.Requests.UserRequests.GetUser
{
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
	}
}
