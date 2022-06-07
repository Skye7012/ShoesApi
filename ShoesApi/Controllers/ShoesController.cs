using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Contracts.Requests.ShoesRequests.GetShoesRequest;
using ShoesApi.Extensions;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Xml.Serialization;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Shoes Controller
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class ShoesController : ControllerBase
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context">DbContext</param>
		public ShoesController(ShoesDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get Shoes
		/// </summary>
		/// <param name="request">Request</param>
		/// <returns>Shoes</returns>
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
					Destination = new GetShoesResponseItemDestination()
					{
						Id = x.Destination!.Id,
						Name = x.Destination!.Name,
					},
					Season = new GetShoesResponseItemSeason()
					{
						Id = x.Season!.Id,
						Name = x.Season!.Name,
					},
					RuSizes = x.Sizes!.Select(x => x.RuSize).ToList(),
				});

			query = query
					.Where(x => request.SearchQuery == null || x.Name.ToLower().Contains(request.SearchQuery.ToLower()))
					.Where(x => request.BrandFilters == null || request.BrandFilters.Contains(x.Brand.Id))
					.Where(x => request.DestinationFilters == null || request.DestinationFilters.Contains(x.Destination.Id))
					.Where(x => request.SeasonFilters == null || request.SeasonFilters.Contains(x.Season.Id))
					.Where(x => request.SizeFilters == null || x.RuSizes
						.Any(y => request.SizeFilters.Contains(y)));

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

		/// <summary>
		/// Get Shoes by ids
		/// </summary>
		/// <param name="ids">Ids</param>
		/// <returns>Shoes</returns>
		[HttpGet("GetByIds")]
		public async Task<GetShoesResponse> GetByIds([FromQuery] int[] ids)
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
					Destination = new GetShoesResponseItemDestination()
					{
						Id = x.Destination!.Id,
						Name = x.Destination!.Name,
					},
					Season = new GetShoesResponseItemSeason()
					{
						Id = x.Season!.Id,
						Name = x.Season!.Name,
					},
					RuSizes = x.Sizes!.Select(x => x.RuSize).ToList(),
				});

			query = query
				.Where(x => ids.Contains(x.Id));

			var count = await query.CountAsync();

			var shoes = await query
				.ToListAsync();


			return new GetShoesResponse()
			{
				Items = shoes,
				TotalCount = count,
			};
		}

		/// <summary>
		/// Get serialized into XML info about shoes 
		/// </summary>
		/// <returns>Serialized into XML info about shoes </returns>
		[HttpGet("GetXml")]
		public async Task<FileStreamResult> Get()
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
					Destination = new GetShoesResponseItemDestination()
					{
						Id = x.Destination!.Id,
						Name = x.Destination!.Name,
					},
					Season = new GetShoesResponseItemSeason()
					{
						Id = x.Season!.Id,
						Name = x.Season!.Name,
					},
					RuSizes = x.Sizes!.Select(x => x.RuSize).ToList(),
				})
				.ToListAsync();

			var response = new GetShoesResponse()
			{
				TotalCount = shoes.Count,
				Items = shoes,
			};

			XmlSerializer xmlSerializer = new XmlSerializer(response.GetType());

			using StringWriter textWriter = new StringWriter();
			xmlSerializer.Serialize(textWriter, response);

			var fileName = "shoes.xml";
			var mimeType = "text/xml";
			var stream = new MemoryStream(Encoding.ASCII.GetBytes(textWriter.ToString()));

			return new FileStreamResult(stream, mimeType)
			{
				FileDownloadName = fileName
			};
		}
	}
}
