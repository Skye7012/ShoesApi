namespace ShoesApi.Requests.OrderRequests
{
	public class OrderPostRequest
	{
		public string Addres { get; set; } = default!;

		public List<OrderPostRequestOrderItem> OrderItems { get; set; }
	}
}
