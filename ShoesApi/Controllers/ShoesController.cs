using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Extensions;
using ShoesApi.Requests.ShoeRequests.GetShoesRequest;
using ShoesApi.Responses.ShoeResponses.GetShoesResponse;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShoesController : ControllerBase
	{
		private readonly ShoesDbContext _context;


		public ShoesController(ShoesDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<GetShoesResponse> Get(
			[FromQuery] GetShoesRequest request)
		{
			var query = _context.Shoes
				.Select(x => new GetShoesResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
					Image = x.Image,
					Price = x.Price,
					Brand = new GetShoesResponseItemBrand()
					{
						Id = x.Brand!.Id,
						Name = x.Brand!.Name,
					},
					Destination = new GetShoesResponseItemDestinaiton()
					{
						Id = x.Destination!.Id,
						Name = x.Destination!.Name,
					},
					Season = new GetShoesResponseItemSeason()
					{
						Id = x.Season!.Id,
						Name = x.Season!.Name,
					},
				});

			if(request.SearchQuery != null)
			{
				query = query
					.Where(x => x.Name.ToLower().Contains(request.SearchQuery.ToLower()));
			}

			if(request.BrandFilters != null)
			{
				query = query
					.Where(x => request.BrandFilters.Contains(x.Brand.Id));
			}

			var count = await query.CountAsync();

			var shoes = await query
				.Sort(request)
				.ToListAsync();


			return new GetShoesResponse()
			{
				Items = shoes,
				TotalCount = count,
			};
		}
	}
}
