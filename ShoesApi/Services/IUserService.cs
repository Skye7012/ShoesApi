namespace ShoesApi.Services
{
	/// <summary>
	/// User service
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Get login by claim in token
		/// </summary>
		/// <returns>login</returns>
		public string GetLogin();
	}
}
