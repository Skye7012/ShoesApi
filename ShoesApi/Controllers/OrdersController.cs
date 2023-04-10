using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Commands.OrderCommands.PostOrder;
using ShoesApi.CQRS.Queries.Order.GetOrders;

namespace ShoesApi.Controllers
{
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
		/// <param name="command">Команда</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Идентификатор созданного заказа</returns>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<int>> PostAsync(
			PostOrderCommand command,
			CancellationToken cancellationToken)
		{
			var orderId = await _mediator.Send(command, cancellationToken);

			return CreatedAtAction(nameof(GetAsync), orderId);
		}
	}
}
