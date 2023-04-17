namespace ShoesApi.Contracts.Requests.Users.SignUpUser;

/// <summary>
/// Ответ на <see cref="SignUpUserRequest"/>
/// </summary>
public class SignUpUserResponse
{
	/// <summary>
	/// Идентификатор зарегистрированного пользователя
	/// </summary>
	public int UserId { get; set; }
}
