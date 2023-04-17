using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Contracts.Requests.Brands.GetBrands;

namespace ShoesApi.Application.Brands.Queries.GetBrands;

/// <summary>
/// Обработчик для of <see cref="GetBrandsQuery"/>
/// </summary>
public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, GetBrandsResponse>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public GetBrandsQueryHandler(IApplicationDbContext context)
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

		var count = await query.CountAsync(cancellationToken);

		var brands = await query
			.OrderBy(x => x.Name)
			.ToListAsync(cancellationToken);

		return new GetBrandsResponse()
		{
			Items = brands,
			TotalCount = count,
		};
	}
}
