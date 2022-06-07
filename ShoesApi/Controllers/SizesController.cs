using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Sizes Controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class SizesController : ControllerBase
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		public SizesController(ShoesDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get Sizes
		/// </summary>
		/// <returns>Sizes</returns>
		[HttpGet]
		public async Task<List<int>> Get()
		{
			return await _context.Sizes
				.Select(x => x.RuSize)
				.ToListAsync();
		}
	}
}
