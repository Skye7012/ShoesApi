namespace ShoesApi.Responses.DestinationResponses.GetDestinationsResponse
{
	public class GetDestinationsResponse
	{
		public int TotalCount { get; set; }
		public List<GetDestinationsResponseItem>? Items { get; set; }
	}
}
