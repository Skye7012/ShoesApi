namespace ShoesApi.Application.Orders.Queries.GetOrders;

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
	/// Идентификатор файла изображения
	/// </summary>
	public int ImageFileId { get; set; }

	/// <summary>
	/// Цена
	/// </summary>
	public int Price { get; set; }
}
