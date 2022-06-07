namespace ShoesApi.Contracts.Requests.UserRequests.SignInRequest
{
	/// <summary>
	/// Response to signIn
	/// </summary>
	public class SignInResponse
	{
		/// <summary>
		/// Authorization token
		/// </summary>
		public string Token { get; set; } = default!;
	}
}
