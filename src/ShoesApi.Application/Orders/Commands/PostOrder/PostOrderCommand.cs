using MediatR;
using ShoesApi.Contracts.Requests.Orders.PostOrder;

namespace ShoesApi.Application.Orders.Commands.PostOrder;

/// <summary>
/// Команда для размещения заказа пользователя
/// </summary>
public class PostOrderCommand : PostOrderRequest, IRequest<int>
{
}
