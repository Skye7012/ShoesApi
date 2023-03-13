using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.CQRS.Queries.Shoes.GetShoesXml.GetShoesByIds
{
	/// <summary>
	/// Обработчик для of <see cref="GetShoesByIdsQuery"/>
	/// </summary>
	public class GetShoesByIdsQueryHandler : IRequestHandler<GetShoesByIdsQuery, GetShoesResponse>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetShoesByIdsQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<GetShoesResponse> Handle(GetShoesByIdsQuery request, CancellationToken cancellationToken)
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
				.Where(x => request.Ids.Contains(x.Id));

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
