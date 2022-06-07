namespace ShoesApi.Contracts
{
	/// <summary>
	/// Base GetReqeust
	/// </summary>
	public class GetRequest
	{
		/// <summary>
		/// Page
		/// </summary>
		public int Page { get; set; } = 1;

		/// <summary>
		/// Limit on page
		/// </summary>
		public int Limit { get; set; }

		/// <summary>
		/// Field for orderBy
		/// </summary>
		public string OrderBy { get; set; } = "Id";

		/// <summary>
		/// IsAscending ordering
		/// </summary>
		public bool IsAscending { get; set; } = true;
	}
}
