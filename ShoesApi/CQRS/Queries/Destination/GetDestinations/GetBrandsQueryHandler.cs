using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.CQRS.Queries.Destination.GetDestinations
{
	/// <summary>
	/// Обработчик для of <see cref="GetDestinationsQuery"/>
	/// </summary>
	public class GetDestinationsQueryHandler : IRequestHandler<GetDestinationsQuery, GetDestinationsResponse>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetDestinationsQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<GetDestinationsResponse> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Destinations
				.Select(x => new GetDestinationsResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
				});

			var count = await query.CountAsync(cancellationToken);

			var brands = await query
				.OrderBy(x => x.Name)
				.ToListAsync(cancellationToken);

			return new GetDestinationsResponse()
			{
				Items = brands,
				TotalCount = count,
			};
		}
	}
}
