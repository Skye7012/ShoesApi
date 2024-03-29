using MediatR;
using ShoesApi.Contracts.Requests.Orders.GetOrders;

namespace ShoesApi.Application.Orders.Queries.GetOrders;

/// <summary>
/// Запрос на получение коллекции заказов пользователя
/// </summary>
public class GetOrdersQuery : IRequest<GetOrdersResponse>
{
}
