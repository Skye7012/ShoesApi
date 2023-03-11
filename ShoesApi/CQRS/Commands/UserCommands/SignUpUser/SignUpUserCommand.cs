using MediatR;

namespace ShoesApi.CQRS.Commands.UserCommands.SignUpUser
{
	/// <summary>
	/// Команда для регистрации пользователя
	/// </summary>
	public class SignUpUserCommand : IRequest<SignUpUserResponse>
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; } = default!;

		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Фамилия
		/// </summary>
		public string? Surname { get; set; }

		/// <summary>
		/// Телефон
		/// </summary>
		public string? Phone { get; set; }
	}
}
