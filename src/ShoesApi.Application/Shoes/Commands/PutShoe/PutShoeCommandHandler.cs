using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Application.Shoes.Commands.PutShoe;

/// <summary>
/// Обработчик для <see cref="PutShoeCommand"/>
/// </summary>
public class PutShoeCommandHandler : IRequestHandler<PutShoeCommand>
{
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	public PutShoeCommandHandler(IApplicationDbContext context)
		=> _context = context;

	/// <inheritdoc/>
	public async Task Handle(PutShoeCommand request, CancellationToken cancellationToken)
	{
		var shoe = await _context.Shoes
			.Include(x => x.Sizes)
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new EntityNotFoundException<Shoe>(request.Id);

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

		shoe.Name = request.Name;
		shoe.Price = request.Price;
		shoe.ImageFile = imageFile;
		shoe.Brand = brand;
		shoe.Destination = destination;
		shoe.Season = season;
		shoe.Sizes = sizes;

		await _context.SaveChangesAsync(cancellationToken);
	}
}
