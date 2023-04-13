namespace ShoesApi.Contracts.Requests.Users.GetUser;

/// <summary>
/// Ответ на запрос получения данных о пользователе
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
