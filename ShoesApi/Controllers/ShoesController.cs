﻿using Microsoft.AspNetCore.Mvc;
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
	}
}
