namespace ShoesApi.Contracts.Requests.OrderRequests.PostOrderRequest
{
	/// <summary>
	/// Request for post order
	/// </summary>
	public class PostOrderRequest
	{
		/// <summary>
		/// Address
		/// </summary>
		public string Address { get; set; } = default!;

		/// <summary>
		/// OrderItems
		/// </summary>
		public List<OrderPostRequestOrderItem> OrderItems { get; set; } = default!;
	}
}
