using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Responses.BrandResponses.GetBrandsResponse;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BrandsController : ControllerBase
	{
		private readonly ShoesDbContext _context;


		public BrandsController(ShoesDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<GetBrandsResponse> Get()
		{
			var query = _context.Brands
				.Select(x => new GetBrandsResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
				});

			var count = await query.CountAsync();

			var brands = await query
				.OrderBy(x => x.Name)
				.ToListAsync();


			return new GetBrandsResponse()
			{
				Items = brands,
				TotalCount = count,
			};
		}
	}
}
