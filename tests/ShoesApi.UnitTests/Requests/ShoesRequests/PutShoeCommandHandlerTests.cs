using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Application.Shoes.Commands.PutShoe;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.ShoesRequests;

/// <summary>
/// Тест для <see cref="PutShoeCommandHandler"/>
/// </summary>
public class PutShoeCommandHandlerTests : UnitTestBase
{
	private readonly File _imageFile;
	private readonly File _imageFile2;
	private readonly List<Size> _sizes;
	private readonly Brand _brand;
	private readonly Brand _brand2;
	private readonly Destination _destination;
	private readonly Destination _destination2;
	private readonly Season _season;
	private readonly Season _season2;
	private readonly Shoe _shoe;
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	public PutShoeCommandHandlerTests()
	{
		_imageFile = new File("test");
		_imageFile2 = new File("testNew");

		_sizes = new List<Size>
		{
			new Size { RuSize = 40 },
			new Size { RuSize = 44 },
		};

		_brand = new Brand { Name = "Adidas" };
		_brand2 = new Brand { Name = "Nike" };

		_destination = new Destination { Name = "Sport" };
		_destination2 = new Destination { Name = "Style" };

		_season = new Season { Name = "Summer" };
		_season2 = new Season { Name = "Winter" };

		_shoe = new Shoe
		{
			Name = "Sneakers",
			ImageFile = _imageFile,
			Price = 100,
			Brand = _brand,
			Destination = _destination,
			Season = _season,
			Sizes = _sizes,
		};

		_context = CreateInMemoryContext(c =>
		{
			c.Files.Add(_imageFile2);
			c.Brands.Add(_brand2);
			c.Destinations.Add(_destination2);
			c.Seasons.Add(_season2);

			c.Shoes.Add(_shoe);
			c.SaveChanges();
		});
	}

	/// <summary>
	/// Должен обновить обувь, когда валиден запрос
	/// </summary>
	[Fact]
	public async Task PutShoeCommand_ShouldCreateShoe_WhenRequestValid()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			Name = "SneakersUpdated",
			ImageFileId = _imageFile2.Id,
			Price = 200,
			BrandId = _brand2.Id,
			DestinationId = _destination2.Id,
			SeasonId = _season2.Id,
			SizesIds = _sizes.Where(x => x.RuSize == 44).Select(x => x.Id).ToHashSet(),
		};

		var handler = new PutShoeCommandHandler(_context);
		await handler.Handle(command, default);

		var updatedShoe = await _context.Shoes
			.Include(x => x.Brand)
			.Include(x => x.Destination)
			.Include(x => x.Season)
			.Include(x => x.Sizes)
			.FirstOrDefaultAsync(x => x.Id == _shoe.Id);

		updatedShoe.Should().NotBeNull();
		updatedShoe!.Name.Should().Be(command.Name);
		updatedShoe!.Price.Should().Be(command.Price);
		updatedShoe!.Brand!.Id.Should().Be(command.BrandId);
		updatedShoe!.Destination!.Id.Should().Be(command.DestinationId);
		updatedShoe!.Season!.Id.Should().Be(command.SeasonId);

		updatedShoe!.Sizes.Should().NotBeNullOrEmpty();
		updatedShoe!.Sizes!.Select(x => x.Id).ToHashSet()
			.Should().Equal(command.SizesIds);
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден файл изображения
	/// </summary>
	[Fact]
	public async Task PutShoeCommandHandler_ShouldThrow_WhenImageFileNotFound()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			ImageFileId = 404
		};

		var handler = new PutShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<File>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден Бренд обуви
	/// </summary>
	[Fact]
	public async Task PutShoeCommandHandler_ShouldThrow_WhenBrandNotFound()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			ImageFileId = _imageFile.Id,
			BrandId = 404
		};

		var handler = new PutShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Brand>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найдено Назначение обуви
	/// </summary>
	[Fact]
	public async Task PutShoeCommandHandler_ShouldThrow_WhenDestinationNotFound()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = 404,
		};

		var handler = new PutShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Destination>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден Сезон обуви
	/// </summary>
	[Fact]
	public async Task PutShoeCommandHandler_ShouldThrow_WhenSeasonNotFound()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = _season.Id,
			SeasonId = 404,
		};

		var handler = new PutShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Season>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда указаны несуществующие размеры
	/// </summary>
	[Fact]
	public async Task PutShoeCommandHandler_ShouldThrow_WhenInvalidSizes()
	{
		var command = new PutShoeCommand(_shoe.Id)
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = _season.Id,
			SeasonId = _season.Id,
			SizesIds = new HashSet<int> { 1, 404, 405 }
		};

		var handler = new PutShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<ApplicationExceptionBase>()
			.WithMessage("Указаны не существующие размеры с id = [404, 405]");
	}
}
