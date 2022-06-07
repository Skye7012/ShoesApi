namespace ShoesApi.Contracts.Requests.OrderRequests.GetOrdersResponse
{
	/// <summary>
	/// OrderItem dto in <see cref="GetOrdersResponseItem"/>
	/// </summary>
	public class GetOrdersResponseItemOrderItem
	{
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// RuSize
		/// </summary>
		public int RuSize { get; set; }

		/// <summary>
		/// Shoe
		/// </summary>
		public GetOrdersResponseItemOrderItemShoe Shoe { get; set; }
	}
}
