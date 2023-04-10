using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Queries.Sizes.GetSizes;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Контроллер Размеров обуви
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class SizesController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="mediator">Медиатор</param>
		public SizesController(IMediator mediator)
			=> _mediator = mediator;

		/// <summary>
		/// Получить список Размеров обуви
		/// </summary>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Список Размеров обуви</returns>
		[HttpGet]
		public async Task<List<int>> GetAsync(CancellationToken cancellationToken)
			=> await _mediator.Send(new GetSizesQuery(), cancellationToken);
	}
}
