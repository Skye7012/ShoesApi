namespace ShoesApi.Contracts.Requests.Orders.GetOrders;

/// <summary>
/// ДТО Заказа из <see cref="GetOrdersResponse"/>
/// </summary>
public class GetOrdersResponseItem
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Дата заказа
	/// </summary>
	public DateTime OrderDate { get; set; }

	/// <summary>
	/// Адрес
	/// </summary>
	public string Address { get; set; } = default!;

	/// <summary>
	/// Итоговая сумма заказа
	/// </summary>
	public int Sum { get; set; }

	/// <summary>
	/// Количество вещей в заказе
	/// </summary>
	public int Count { get; set; }

	/// <summary>
	/// Заказанные кроссовки
	/// </summary>
	public List<GetOrdersResponseItemOrderItem>? OrderItems { get; set; }
}
