using MediatR;

namespace ShoesApi.Application.Orders.Commands.PostOrder;

/// <summary>
/// Команда для размещения заказа пользователя
/// </summary>
public class PostOrderCommand : IRequest<int>
{
	/// <summary>
	/// Адрес
	/// </summary>
	public string Address { get; set; } = default!;

	/// <summary>
	/// Заказанная обувь
	/// </summary>
	public List<PostOrderCommandOrderItem> OrderItems { get; set; } = default!;
}
