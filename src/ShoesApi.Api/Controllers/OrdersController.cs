using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Orders.Commands.PostOrder;
using ShoesApi.Application.Orders.Queries.GetOrders;
using ShoesApi.Contracts.Requests.Orders.GetOrders;
using ShoesApi.Contracts.Requests.Orders.PostOrder;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер Заказов
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public OrdersController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Получить заказы пользователя
	/// </summary>
	/// /// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Заказы пользователя</returns>
	[HttpGet]
	[Authorize]
	public async Task<GetOrdersResponse> GetAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetOrdersQuery(), cancellationToken);

	/// <summary>
	/// Создать заказ
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Идентификатор созданного заказа</returns>
	[HttpPost]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<int>> PostAsync(
		PostOrderRequest request,
		CancellationToken cancellationToken)
	{
		var orderId = await _mediator.Send(
			new PostOrderCommand
			{
				Address = request.Address,
				OrderItems = request.OrderItems,
			},
			cancellationToken);

		return CreatedAtAction(nameof(GetAsync), orderId);
	}
}
