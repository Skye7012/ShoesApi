using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Extensions;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoes
{
	/// <summary>
	/// Обработчик для <see cref="GetShoesQuery"/>
	/// </summary>
	public class GetShoesQueryHandler : IRequestHandler<GetShoesQuery, GetShoesResponse>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetShoesQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<GetShoesResponse> Handle(GetShoesQuery request, CancellationToken cancellationToken)
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
	}
}
