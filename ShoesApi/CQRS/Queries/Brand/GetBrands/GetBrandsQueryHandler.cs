using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShoesApi.CQRS.Queries.Brand.GetBrands
{
	/// <summary>
	/// Обработчик для of <see cref="GetBrandsQuery"/>
	/// </summary>
	public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, GetBrandsResponse>
	{
		private readonly ShoesDbContext _context;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		public GetBrandsQueryHandler(ShoesDbContext context)
			=> _context = context;

		/// <inheritdoc/>
		public async Task<GetBrandsResponse> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Brands
				.Select(x => new GetBrandsResponseItem()
				{
					Id = x.Id,
					Name = x.Name,
				});

			var count = await query.CountAsync();

			var brands = await query
				.OrderBy(x => x.Name)
				.ToListAsync();

			return new GetBrandsResponse()
			{
				Items = brands,
				TotalCount = count,
			};
		}
	}
}
