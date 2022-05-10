using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Extensions;
using ShoesApi.Responses.DestinationResponses.GetDestinationsResponse;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DestinationsController : ControllerBase
	{
		private readonly ShoesDbContext _context;


		public DestinationsController(ShoesDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<GetDestinationsResponse> Get()
		{
			var query = _context.Destinations
				.Select(x => new GetDestinationsResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
				});

			var count = await query.CountAsync();

			var brands = await query
				.OrderBy(x => x.Name)
				.ToListAsync();


			return new GetDestinationsResponse()
			{
				Items = brands,
				TotalCount = count,
			};
		}
	}
}
