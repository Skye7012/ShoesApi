namespace ShoesApi.Entities
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User : EntityBase
	{
		/// <summary>
		/// Логин
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Хэш пароля
		/// </summary>
		public byte[] PasswordHash { get; set; } = default!;

		/// <summary>
		/// Соль пароля
		/// </summary>
		public byte[] PasswordSalt { get; set; } = default!;

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

		#region navigation Properties

		/// <summary>
		/// Заказы
		/// </summary>
		public List<Order>? Orders { get; set; }

		#endregion

	}
}
