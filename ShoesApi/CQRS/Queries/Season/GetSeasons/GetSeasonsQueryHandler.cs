using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.CQRS.Queries.Season.GetSeasons
{
	/// <summary>
	/// Обработчик для of <see cref="GetSeasonsQuery"/>
	/// </summary>
	public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, GetSeasonsResponse>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetSeasonsQueryHandler(ShoesDbContext context) 
			=> _context = context;

		/// <inheritdoc/>
		public async Task<GetSeasonsResponse> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
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
