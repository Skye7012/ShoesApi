using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Queries.Shoes.GetShoes;
using ShoesApi.CQRS.Queries.Shoes.GetShoes.GetShoesXml;
using ShoesApi.CQRS.Queries.Shoes.GetShoes.GetShoesByIds;

namespace ShoesApi.Controllers
{
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
		/// <returns>Список обуви</returns>
		[HttpGet]
		public async Task<GetShoesResponse> Get(
			[FromQuery] GetShoesQuery request)
			=> await _mediator.Send(request);

		/// <summary>
		/// Получить список обуви по коллекции идентификаторов
		/// </summary>
		/// <param name="ids">Коллекция идентификаторов</param>
		/// <returns>Список обуви по коллекции идентификаторов</returns>
		[HttpGet("GetByIds")]
		public async Task<GetShoesResponse> GetByIds([FromQuery] int[] ids)
			=> await _mediator.Send(new GetShoesByIdsQuery(ids));

		/// <summary>
		/// Получить коллекцию обуви в формате XML
		/// </summary>
		/// <returns>Коллекция обуви в формате XML </returns>
		[HttpGet("GetXml")]
		public async Task<FileStreamResult> Get()
			=> await _mediator.Send(new GetShoesXmlQuery());
	}
}
