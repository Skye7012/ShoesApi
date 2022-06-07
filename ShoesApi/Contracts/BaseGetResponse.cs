namespace ShoesApi.Contracts
{
	/// <summary>
	/// Base class for response to get request
	/// </summary>
	public class BaseGetResponse<T>
		where T : class
	{
		/// <summary>
		/// Total count of items
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Response items
		/// </summary>
		public List<T>? Items { get; set; }
	}
}
