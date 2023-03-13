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
		/// <returns>Заказы пользователя</returns>
		[HttpGet]
		[Authorize]
		public async Task<GetOrdersResponse> Get()
			=> await _mediator.Send(new GetOrdersQuery());

		/// <summary>
		/// Создать заказ
		/// </summary>
		/// <param name="command">Команда</param>
		/// <returns>Идентификатор созданного заказа</returns>
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<int>> Post(PostOrderCommand command)
		{
			var orderId = await _mediator.Send(command);

			return Ok(orderId);
		}
	}
}
