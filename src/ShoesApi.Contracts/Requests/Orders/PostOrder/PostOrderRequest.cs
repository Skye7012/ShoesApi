namespace ShoesApi.Contracts.Requests.Orders.PostOrder;

/// <summary>
/// Запрос для размещения заказа пользователя
/// </summary>
public class PostOrderRequest
{
	/// <summary>
	/// Адрес
	/// </summary>
	public string Address { get; set; } = default!;

	/// <summary>
	/// Заказанная обувь
	/// </summary>
	public List<PostOrderRequestOrderItem> OrderItems { get; set; } = default!;
}
