namespace ShoesApi.CQRS.Queries.Order.GetOrders
{
	/// <summary>
	/// ДТО Части заказа из <see cref="GetOrdersResponseItem"/>
	/// </summary>
	public class GetOrdersResponseItemOrderItem
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Российский размер обуви
		/// </summary>
		public int RuSize { get; set; }

		/// <summary>
		/// Обувь
		/// </summary>
		public GetOrdersResponseItemOrderItemShoe Shoe { get; set; } = default!;
	}
}
