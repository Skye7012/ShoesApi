using MediatR;
using ShoesApi.Contracts.Requests.Users.SignInUser;

namespace ShoesApi.Application.Users.Commands.SignInUser;

/// <summary>
/// Команда для авторизации пользователя
/// </summary>
public class SignInUserCommand : SignInUserRequest, IRequest<SignInUserResponse>
{
}
