using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Contracts.Requests.Seasons.GetSeasons;

namespace ShoesApi.Application.Seasons.Queries.GetSeasons;

/// <summary>
/// Обработчик для of <see cref="GetSeasonsQuery"/>
/// </summary>
public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, GetSeasonsResponse>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public GetSeasonsQueryHandler(IApplicationDbContext context)
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

		var count = await query.CountAsync(cancellationToken);

		var seasons = await query
			.OrderBy(x => x.Name)
			.ToListAsync(cancellationToken);

		return new GetSeasonsResponse()
		{
			Items = seasons,
			TotalCount = count,
		};
	}
}
