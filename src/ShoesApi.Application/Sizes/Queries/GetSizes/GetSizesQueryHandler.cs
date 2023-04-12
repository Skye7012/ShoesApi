using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Application.Sizes.Queries.GetSizes;

/// <summary>
/// Обработчик для of <see cref="GetSizesQuery"/>
/// </summary>
public class GetSizesQueryHandler : IRequestHandler<GetSizesQuery, List<int>>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public GetSizesQueryHandler(IApplicationDbContext context)
		=> _context = context;

	/// <inheritdoc/>
	public async Task<List<int>> Handle(GetSizesQuery request, CancellationToken cancellationToken)
		=> await _context.Sizes
			.Select(x => x.RuSize)
			.ToListAsync(cancellationToken);
}
