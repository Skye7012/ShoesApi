using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Contracts.Requests.BrandRequests.GetBrandsRequest;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Brands controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class BrandsController : ControllerBase
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		public BrandsController(ShoesDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get brands
		/// </summary>
		/// <returns>Brands</returns>
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
