using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ShoesApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SizesController : ControllerBase
	{
		private readonly ShoesDbContext _context;


		public SizesController(ShoesDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<List<int>> Get()
		{
			return await _context.Sizes
				.Select(x => x.RuSize)
				.ToListAsync();
		}
	}
}
