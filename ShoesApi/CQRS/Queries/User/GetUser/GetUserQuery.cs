using MediatR;

namespace ShoesApi.CQRS.Queries.User.GetUser
{
	/// <summary>
	/// Запрос на получение данных о пользователе
	/// </summary>
	public class GetUserQuery : IRequest<GetUserResponse>
	{
	}
}
