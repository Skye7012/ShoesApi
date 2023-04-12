using ShoesApi.Domain.Common;

namespace ShoesApi.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : EntityBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="login">Логин</param>
	/// <param name="passwordHash">Хэш пароля</param>
	/// <param name="passwordSalt">Соль пароля</param>
	/// <param name="name">Имя</param>
	/// <param name="surname">Фамилия</param>
	/// <param name="phone">Телефон</param>
	/// <param name="orders">Заказы</param>
	public User(
		string login,
		byte[] passwordHash,
		byte[] passwordSalt,
		string name,
		string? surname,
		string? phone,
		List<Order>? orders = null)
	{
		Login = login;
		PasswordHash = passwordHash;
		PasswordSalt = passwordSalt;
		Name = name;
		Surname = surname;
		Phone = phone;

		Orders = orders ?? new List<Order>();
	}

	/// <summary>
	/// Конструктор
	/// </summary>
	public User() { }

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
	public List<Order>? Orders { get; private set; }

	#endregion
}
