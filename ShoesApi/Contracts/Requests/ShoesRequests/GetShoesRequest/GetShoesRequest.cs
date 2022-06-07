namespace ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest
{
	/// <summary>
	/// Get Shoes Request 
	/// </summary>
	public class GetShoesRequest : GetRequest
	{
		/// <summary>
		/// Filter by search
		/// </summary>
		public string? SearchQuery { get; set; }

		/// <summary>
		/// Filter by brands
		/// </summary>
		public List<int>? BrandFilters { get; set; }

		/// <summary>
		/// Filter by destination
		/// </summary>
		public List<int>? DestinationFilters { get; set; }

		/// <summary>
		/// Filter by seasons
		/// </summary>
		public List<int>? SeasonFilters { get; set; }

		/// <summary>
		/// Filter by sizes
		/// </summary>
		public List<int>? SizeFilters { get; set; }
	}
}
