using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Shoes.Queries.Common;
using ShoesApi.Application.Shoes.Queries.GetShoes;
using ShoesApi.Application.Shoes.Queries.GetShoesByIds;
using ShoesApi.Application.Shoes.Queries.GetShoesXml;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер Обуви
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShoesController : ControllerBase
{
	private readonly IMediator _mediator;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="mediator">Медиатор</param>
	public ShoesController(IMediator mediator)
		=> _mediator = mediator;

	/// <summary>
	/// Получить список обуви
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Список обуви</returns>
	[HttpGet]
	public async Task<GetShoesResponse> GetAsync(
		[FromQuery] GetShoesQuery request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(request, cancellationToken);

	/// <summary>
	/// Получить список обуви по коллекции идентификаторов
	/// </summary>
	/// <param name="ids">Коллекция идентификаторов</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Список обуви по коллекции идентификаторов</returns>
	[HttpGet("GetByIds")]
	public async Task<GetShoesResponse> GetByIdsAsync(
		[FromQuery] int[] ids,
		CancellationToken cancellationToken)
			=> await _mediator.Send(new GetShoesByIdsQuery(ids), cancellationToken);

	/// <summary>
	/// Получить коллекцию обуви в формате XML
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Коллекция обуви в формате XML </returns>
	[HttpGet("GetXml")]
	public async Task<FileStreamResult> GetXmlAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetShoesXmlQuery(), cancellationToken);
}
