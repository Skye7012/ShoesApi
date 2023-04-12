namespace ShoesApi.Application.Users.Commands.SignUpUser;

/// <summary>
/// Ответ на <see cref="SignUpUserCommand"/>
/// </summary>
public class SignUpUserResponse
{
	/// <summary>
	/// Идентификатор зарегистрированного пользователя
	/// </summary>
	public int UserId { get; set; }
}
