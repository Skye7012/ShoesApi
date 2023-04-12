using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Destinations.Queries.GetDestinations;

namespace ShoesApi.Api.Controllers;

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
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Список Назначений обуви</returns>
	[HttpGet]
	public async Task<GetDestinationsResponse> GetAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetDestinationsQuery(), cancellationToken);
}
