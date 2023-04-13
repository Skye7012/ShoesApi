using MediatR;
using ShoesApi.Contracts.Requests.Users.SignUpUser;

namespace ShoesApi.Application.Users.Commands.SignUpUser;

/// <summary>
/// Команда для регистрации пользователя
/// </summary>
public class SignUpUserCommand : SignUpUserRequest, IRequest<SignUpUserResponse>
{
}
