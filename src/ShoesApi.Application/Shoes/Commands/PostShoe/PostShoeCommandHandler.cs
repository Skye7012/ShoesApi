using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Application.Shoes.Commands.PostShoe;

/// <summary>
/// Обработчик для <see cref="PostShoeCommand"/>
/// </summary>
public class PostShoeCommandHandler : IRequestHandler<PostShoeCommand, int>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public PostShoeCommandHandler(IApplicationDbContext context)
		=> _context = context;

	/// <inheritdoc/>
	public async Task<int> Handle(PostShoeCommand request, CancellationToken cancellationToken)
	{
		var imageFile = await _context.Files
			.FirstOrDefaultAsync(x => x.Id == request.ImageFileId, cancellationToken)
			?? throw new EntityNotFoundException<File>(request.ImageFileId);

		var brand = await _context.Brands
			.FirstOrDefaultAsync(x => x.Id == request.BrandId, cancellationToken)
			?? throw new EntityNotFoundException<Brand>(request.BrandId);

		var destination = await _context.Destinations
			.FirstOrDefaultAsync(x => x.Id == request.DestinationId, cancellationToken)
			?? throw new EntityNotFoundException<Destination>(request.DestinationId);

		var season = await _context.Seasons
			.FirstOrDefaultAsync(x => x.Id == request.SeasonId, cancellationToken)
			?? throw new EntityNotFoundException<Season>(request.SeasonId);

		var sizes = await _context.Sizes
			.Where(x => request.SizesIds.Contains(x.Id))
			.ToListAsync(cancellationToken);

		var notFoundSizes = request.SizesIds
			.Where(sid => sizes.All(s => s.Id != sid));

		if (notFoundSizes.Any())
			throw new ApplicationExceptionBase("Указаны не существующие размеры с id = " +
				$"[{string.Join(", ", notFoundSizes)}]");

		var shoe = new Shoe()
		{
			Name = request.Name,
			Price = request.Price,
			ImageFile = imageFile,
			Brand = brand,
			Destination = destination,
			Season = season,
			Sizes = sizes,
		};

		await _context.Shoes.AddAsync(shoe, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return shoe.Id;
	}
}
