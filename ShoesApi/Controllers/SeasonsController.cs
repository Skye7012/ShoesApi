using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Contracts.Requests.SeasonRequests.GetSeasonsRequest;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Seasons Controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class SeasonsController : ControllerBase
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		public SeasonsController(ShoesDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get Seasons
		/// </summary>
		/// <returns>Seasons</returns>
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
