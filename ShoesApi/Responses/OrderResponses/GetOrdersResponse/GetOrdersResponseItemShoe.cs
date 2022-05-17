namespace ShoesApi.Responses.OrderResponses.GetOrdersResponse
{
	public class GetOrdersResponseItemShoe
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Image { get; set; } = default!;
		public int Price { get; set; }
	}
}
