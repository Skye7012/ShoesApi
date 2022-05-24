using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Responses.SeasonResponses.GetSeasonsResponse;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SeasonsController : ControllerBase
	{
		private readonly ShoesDbContext _context;


		public SeasonsController(ShoesDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<GetSeasonsResponse> Get()
		{
			var query = _context.Seasons
				.Select(x => new GetSeasonsResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
				});

			var count = await query.CountAsync();

			var brands = await query
				.OrderBy(x => x.Name)
				.ToListAsync();


			return new GetSeasonsResponse()
			{
				Items = brands,
				TotalCount = count,
			};
		}
	}
}
