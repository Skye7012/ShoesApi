namespace ShoesApi.Contracts.Requests.Users.PutUser;

/// <summary>
/// Запрос на обновление данных о пользователе
/// </summary>
public class PutUserRequest
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
