namespace ShoesApi.Contracts.Requests.OrderRequests.GetOrdersResponse
{
	/// <summary>
	/// Shoe dto in <see cref="GetOrdersResponseItemOrderItem"/>
	/// </summary>
	public class GetOrdersResponseItemOrderItemShoe
	{
		/// <summary>
		/// ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Image name (for path)
		/// </summary>
		public string Image { get; set; } = default!;

		/// <summary>
		/// Price
		/// </summary>
		public int Price { get; set; }
	}
}
