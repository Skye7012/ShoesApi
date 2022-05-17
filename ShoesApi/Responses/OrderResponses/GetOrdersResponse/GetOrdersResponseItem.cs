namespace ShoesApi.Responses.OrderResponses.GetOrdersResponse
{
	public class GetOrdersResponseItem
	{
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }

		public int Sum { get; set; }

		public int Count { get; set; }
		public List<GetOrdersResponseItemOrderItem>? OrderItems { get; set; }
	}
}
