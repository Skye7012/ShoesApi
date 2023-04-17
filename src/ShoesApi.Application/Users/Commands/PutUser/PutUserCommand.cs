using MediatR;
using ShoesApi.Contracts.Requests.Users.PutUser;

namespace ShoesApi.Application.Users.Commands.PutUser;

/// <summary>
/// Команда на обновление данных о пользователе
/// </summary>
public class PutUserCommand : PutUserRequest, IRequest
{
}
