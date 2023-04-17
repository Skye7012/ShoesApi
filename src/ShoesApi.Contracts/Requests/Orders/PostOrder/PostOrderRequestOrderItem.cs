namespace ShoesApi.Contracts.Requests.Orders.PostOrder;

/// <summary>
/// ДТО Части заказа из <see cref="PostOrderRequest"/>
/// </summary>
public class PostOrderRequestOrderItem
{
	/// <summary>
	/// Идентификатор обуви
	/// </summary>
	public int ShoeId { get; set; }

	/// <summary>
	/// Выбранный Российский размер
	/// </summary>
	public int RuSize { get; set; }
}
