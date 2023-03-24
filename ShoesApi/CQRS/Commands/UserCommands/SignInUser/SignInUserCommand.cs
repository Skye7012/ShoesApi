using MediatR;

namespace ShoesApi.CQRS.Commands.UserCommands.SignInUser
{
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
}
