namespace ShoesApi.CQRS.Commands.UserCommands.SignInUser
{
	/// <summary>
	/// Ответ на <see cref="SignInUserCommand"/>
	/// </summary>
	public class SignInUserResponse
	{
		/// <summary>
		/// Токен авторизации
		/// </summary>
		public string Token { get; set; } = default!;
	}
}
