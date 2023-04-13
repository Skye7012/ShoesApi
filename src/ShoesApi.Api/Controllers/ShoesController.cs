using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Shoes.Commands.DeleteShoe;
using ShoesApi.Application.Shoes.Commands.PostShoe;
using ShoesApi.Application.Shoes.Commands.PutShoe;
using ShoesApi.Application.Shoes.Queries.GetShoes;
using ShoesApi.Application.Shoes.Queries.GetShoesByIds;
using ShoesApi.Application.Shoes.Queries.GetShoesXml;
using ShoesApi.Contracts.Requests.Shoes.Common;
using ShoesApi.Contracts.Requests.Shoes.GetShoes;
using ShoesApi.Contracts.Requests.Shoes.PostShoe;
using ShoesApi.Contracts.Requests.Shoes.PutShoe;

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
		[FromQuery] GetShoesRequest request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(
			new GetShoesQuery
			{
				BrandFilters = request.BrandFilters,
				DestinationFilters = request.DestinationFilters,
				IsAscending = request.IsAscending,
				Limit = request.Limit,
				OrderBy = request.OrderBy,
				Page = request.Page,
				SearchQuery = request.SearchQuery,
				SeasonFilters = request.SeasonFilters,
				SizeFilters = request.SizeFilters,
			},
			cancellationToken);

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

	/// <summary>
	/// Создать обувь
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Идентификатор созданной обуви</returns>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<int>> PostAsync(
		PostShoeRequest request,
		CancellationToken cancellationToken)
			=> CreatedAtAction(
				nameof(GetAsync),
				await _mediator.Send(
					new PostShoeCommand
					{
						BrandId = request.BrandId,
						DestinationId = request.DestinationId,
						ImageFileId = request.ImageFileId,
						Name = request.Name,
						Price = request.Price,
						SeasonId = request.SeasonId,
						SizesIds = request.SizesIds,
					},
					cancellationToken));

	/// <summary>
	/// Обновить данные об обуви
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task PutAsync(
		[FromRoute] int id,
		[FromBody] PutShoeRequest request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(
			new PutShoeCommand(id)
			{
				BrandId = request.BrandId,
				DestinationId = request.DestinationId,
				SizesIds = request.SizesIds,
				SeasonId = request.SeasonId,
				Price = request.Price,
				Name = request.Name,
				ImageFileId = request.ImageFileId,
			},
			cancellationToken);

	/// <summary>
	/// Удалить обувь
	/// </summary>
	/// <param name="id">Идентификатор</param>
	/// <param name="cancellationToken">Токен отмены</param>
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task DeleteAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken)
		=> await _mediator.Send(new DeleteShoeCommand(id), cancellationToken);
}
