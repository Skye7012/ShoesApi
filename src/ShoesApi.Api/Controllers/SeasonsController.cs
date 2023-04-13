using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Seasons.Queries.GetSeasons;
using ShoesApi.Contracts.Requests.Seasons.GetSeasons;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер Сезонов обуви
/// </summary>
[ApiController]
[Route("[controller]")]
public class SeasonsController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public SeasonsController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Получить список Сезонов обуви
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Список Сезонов обуви</returns>
	[HttpGet]
	public async Task<GetSeasonsResponse> GetAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetSeasonsQuery(), cancellationToken);
}
