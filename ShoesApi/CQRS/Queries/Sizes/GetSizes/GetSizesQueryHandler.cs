using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.CQRS.Queries.Sizes.GetSizes
{
	/// <summary>
	/// Обработчик для of <see cref="GetSizesQuery"/>
	/// </summary>
	public class GetSizesQueryHandler : IRequestHandler<GetSizesQuery, List<int>>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetSizesQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<List<int>> Handle(GetSizesQuery request, CancellationToken cancellationToken)
			=> await _context.Sizes
				.Select(x => x.RuSize)
				.ToListAsync();
	}
}
