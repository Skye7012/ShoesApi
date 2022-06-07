namespace ShoesApi.Contracts.Requests.UserRequests.UserGetRequest
{
	/// <summary>
	/// Response to get user credentials
	/// </summary>
	public class UserGetResponse
	{
		/// <summary>
		/// Login
		/// </summary>
		public string Login { get; set; } = default!;

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// First name
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// Phone
		/// </summary>
		public string? Phone { get; set; }
	}
}
