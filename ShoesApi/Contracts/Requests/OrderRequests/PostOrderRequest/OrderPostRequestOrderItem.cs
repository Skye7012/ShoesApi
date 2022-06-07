namespace ShoesApi.Contracts.Requests.OrderRequests.PostOrderRequest
{
	/// <summary>
	/// Part of order in <see cref="PostOrderRequest"/>
	/// </summary>
	public class OrderPostRequestOrderItem
	{
		/// <summary>
		/// ShoeId
		/// </summary>
		public int ShoeId { get; set; }

		/// <summary>
		/// RuSize
		/// </summary>
		public int RuSize { get; set; }
	}
}
