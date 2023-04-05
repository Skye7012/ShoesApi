using MediatR;

namespace ShoesApi.CQRS.Queries.Order.GetOrders
{
	/// <summary>
	/// Запрос на получение коллекции заказов пользователя
	/// </summary>
	public class GetOrdersQuery : IRequest<GetOrdersResponse>
	{
	}
}
