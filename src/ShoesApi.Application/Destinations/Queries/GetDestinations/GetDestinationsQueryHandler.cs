using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Application.Destinations.Queries.GetDestinations;

/// <summary>
/// Обработчик для of <see cref="GetDestinationsQuery"/>
/// </summary>
public class GetDestinationsQueryHandler : IRequestHandler<GetDestinationsQuery, GetDestinationsResponse>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public GetDestinationsQueryHandler(IApplicationDbContext context)
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

		var destinations = await query
			.OrderBy(x => x.Name)
			.ToListAsync(cancellationToken);

		return new GetDestinationsResponse()
		{
			Items = destinations,
			TotalCount = count,
		};
	}
}
