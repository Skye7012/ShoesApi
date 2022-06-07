namespace ShoesApi.Contracts.Requests.UserRequests.SignUpRequest
{
	/// <summary>
	/// Request to signUp
	/// </summary>
	public class SignUpRequest
	{
		/// <summary>
		/// Login
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// First Name
		/// </summary>

		public string? FirstName { get; set; }

		/// <summary>
		/// Phone
		/// </summary>
		public string? Phone { get; set; }
	}
}
