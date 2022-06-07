namespace ShoesApi.Contracts.Requests.OrderRequests.GetOrdersResponse
{
	/// <summary>
	/// Order dto in <see cref="GetOrdersResponse"/>
	/// </summary>
	public class GetOrdersResponseItem
	{
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// OrderDate
		/// </summary>
		public DateTime OrderDate { get; set; }

		/// <summary>
		/// Address
		/// </summary>
		public string Address { get; set; } = default!;

		/// <summary>
		/// Sum
		/// </summary>
		public int Sum { get; set; }

		/// <summary>
		/// Count
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// Order Items
		/// </summary>
		public List<GetOrdersResponseItemOrderItem>? OrderItems { get; set; }
	}
}
