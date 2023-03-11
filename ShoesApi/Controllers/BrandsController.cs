using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Queries.Brand.GetBrands;

namespace ShoesApi.Controllers
{
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
		/// <returns>Список Брэндов обуви</returns>
		[HttpGet]
		public async Task<GetBrandsResponse> Get()
			=> await _mediator.Send(new GetBrandsQuery());
	}
}
