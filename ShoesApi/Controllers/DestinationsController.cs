using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Contracts.Requests.DestinationRequests.GetDestinationsRequest;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Destinations Controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class DestinationsController : ControllerBase
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		public DestinationsController(ShoesDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get Destinations
		/// </summary>
		/// <returns>Destinations</returns>
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
