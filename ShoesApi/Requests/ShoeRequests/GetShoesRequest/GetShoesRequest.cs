namespace ShoesApi.Requests.ShoeRequests.GetShoesRequest
{
	public class GetShoesRequest : GetRequest
	{
		public string? SearchQuery { get; set; }

		public List<int>? BrandFilters { get; set; }
		public List<int>? DestinationFilters { get; set; }
		public List<int>? SeasonFilters { get; set; }
	}
}
