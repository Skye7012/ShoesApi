using MediatR;

namespace ShoesApi.Application.Users.Queries.GetUser;

/// <summary>
/// Запрос на получение данных о пользователе
/// </summary>
public class GetUserQuery : IRequest<GetUserResponse>
{
}
