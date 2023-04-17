namespace ShoesApi.Contracts.Requests.Users.SignUpUser;

/// <summary>
/// Запрос для регистрации пользователя
/// </summary>
public class SignUpUserRequest
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
