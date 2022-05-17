namespace ShoesApi.Responses.OrderResponses.GetOrdersResponse
{
	public class GetOrdersResponseItemOrderItem
	{
		public int Id { get; set; }
		public int RuSize { get; set; }
		public GetOrdersResponseItemShoe Shoe { get; set; }
	}
}
