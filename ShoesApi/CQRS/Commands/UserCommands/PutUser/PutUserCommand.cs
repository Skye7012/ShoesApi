using MediatR;

namespace ShoesApi.CQRS.Commands.UserCommands.PutUser
{
	/// <summary>
	/// Команда на обновление данных о пользователе
	/// </summary>
	public class PutUserCommand : IRequest
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string? Name { get; set; }

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
