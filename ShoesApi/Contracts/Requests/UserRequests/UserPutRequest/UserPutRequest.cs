namespace ShoesApi.Requests.UserRequests
{
	/// <summary>
	/// Request to update User Credentials
	/// </summary>
	public class UserPutRequest
	{
		/// <summary>
		/// Name
		/// </summary>
		public string? Name { get; set; }

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
