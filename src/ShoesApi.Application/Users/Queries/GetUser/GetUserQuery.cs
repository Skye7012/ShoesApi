using MediatR;
using ShoesApi.Contracts.Requests.Users.GetUser;

namespace ShoesApi.Application.Users.Queries.GetUser;

/// <summary>
/// Запрос на получение данных о пользователе
/// </summary>
public class GetUserQuery : IRequest<GetUserResponse>
{
}
