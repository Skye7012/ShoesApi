using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Brands.Queries.GetBrands;
using ShoesApi.Contracts.Requests.Brands.GetBrands;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер Брэндов обуви
/// </summary>
[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public BrandsController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Получить список Брэндов обуви
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Список Брэндов обуви</returns>
	[HttpGet]
	public async Task<GetBrandsResponse> GetAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetBrandsQuery(), cancellationToken);
}
