namespace ShoesApi.Contracts.Requests.Users.SignInUser;

/// <summary>
/// Запрос на авторизацию пользователя
/// </summary>
public class SignInUserRequest
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
