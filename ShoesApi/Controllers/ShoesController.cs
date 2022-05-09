using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Responses;
using ShoesApi.Responses.ShoeResponse;

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
		public async Task<GetShoesResponse> Get()
		{
			var shoes = await _context.Shoes
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
				})
				.ToListAsync();

			return new GetShoesResponse()
			{
				Items = shoes,
				TotalCount = shoes.Count,
			};
		}
	}
}
