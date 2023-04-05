using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Queries.Destination.GetDestinations;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Контроллер Назначений обуви
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class DestinationsController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="mediator">Медиатор</param>
		public DestinationsController(IMediator mediator)
			=> _mediator = mediator;

		/// <summary>
		/// Получить список Назначений обуви
		/// </summary>
		/// <returns>Список Назначений обуви</returns>
		[HttpGet]
		public async Task<GetDestinationsResponse> Get()
			=> await _mediator.Send(new GetDestinationsQuery());
	}
}
