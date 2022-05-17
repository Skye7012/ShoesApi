namespace ShoesApi.Responses.OrderResponses.GetOrdersResponse
{
	public class GetOrdersResponse
	{
		public int TotalCount { get; set; }
		public List<GetOrdersResponseItem>? Items { get; set; }
	}
}
