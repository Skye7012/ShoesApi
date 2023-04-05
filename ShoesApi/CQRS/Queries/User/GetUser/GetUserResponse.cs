namespace ShoesApi.CQRS.Queries.User.GetUser
{
	/// <summary>
	/// Ответ на <see cref="GetUserQuery"/>
	/// </summary>
	public class GetUserResponse
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; } = default!;

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
