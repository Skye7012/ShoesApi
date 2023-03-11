namespace ShoesApi.CQRS.Queries.Order.GetOrders
{
	/// <summary>
	/// ДТО Обуви из <see cref="GetOrdersResponseItemOrderItem"/>
	/// </summary>
	public class GetOrdersResponseItemOrderItemShoe
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Наименование
		/// </summary>
		public string Name { get; set; } = default!;

		/// <summary>
		/// Путь для изображения // TODO: change
		/// </summary>
		public string Image { get; set; } = default!;

		/// <summary>
		/// Цена
		/// </summary>
		public int Price { get; set; }
	}
}
