namespace ShoesApi.Contracts.Requests.Users.SignInUser;

/// <summary>
/// Ответ на <see cref="SignInUserRequest"/>
/// </summary>
public class SignInUserResponse
{
	/// <summary>
	/// Токен авторизации
	/// </summary>
	public string Token { get; set; } = default!;
}
