using MediatR;

namespace ShoesApi.Application.Users.Commands.SignInUser;

/// <summary>
/// Команда для авторизации пользователя
/// </summary>
public class SignInUserCommand : IRequest<SignInUserResponse>
{
	/// <summary>
	/// Логин
	/// </summary>
	public string Login { get; set; } = default!;

	/// <summary>
	/// Пароль
	/// </summary>
	public string Password { get; set; } = default!;
}
