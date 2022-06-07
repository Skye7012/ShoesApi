namespace ShoesApi.Contracts.Requests.UserRequests.SignInRequest
{
	/// <summary>
	/// Request to signIn
	/// </summary>
	public class SignInRequest
	{
		/// <summary>
		/// Login
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; } = default!;
	}
}
