using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Application.Shoes.Commands.PostShoe;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.ShoesRequests;

/// <summary>
/// Тест для <see cref="PostShoeCommandHandler"/>
/// </summary>
public class PostShoeCommandHandlerTests : UnitTestBase
{
	private readonly File _imageFile;
	private readonly List<Size> _sizes;
	private readonly Brand _brand;
	private readonly Destination _destination;
	private readonly Season _season;
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	public PostShoeCommandHandlerTests()
	{
		_imageFile = new File("test");

		_sizes = new List<Size>
		{
			new Size { RuSize = 40 },
			new Size { RuSize = 44 },
		};

		_brand = new Brand { Name = "Adidas" };

		_destination = new Destination { Name = "Sport" };

		_season = new Season { Name = "Summer" };

		_context = CreateInMemoryContext(c =>
		{
			c.Files.Add(_imageFile);
			c.Sizes.AddRange(_sizes);
			c.Brands.Add(_brand);
			c.Destinations.Add(_destination);
			c.Seasons.Add(_season);
			c.SaveChanges();
		});
	}

	/// <summary>
	/// Должен создать обувь, когда валиден запрос
	/// </summary>
	[Fact]
	public async Task PostShoeCommand_ShouldCreateShoe_WhenRequestValid()
	{
		var command = new PostShoeCommand
		{
			Name = "Sneakers",
			ImageFileId = _imageFile.Id,
			Price = 100,
			BrandId = _brand.Id,
			DestinationId = _destination.Id,
			SeasonId = _season.Id,
			SizesIds = _sizes.Select(x => x.Id).ToHashSet(),
		};

		var handler = new PostShoeCommandHandler(_context);
		var shoeId = await handler.Handle(command, default);

		var createdShoe = await _context.Shoes
			.Include(x => x.Brand)
			.Include(x => x.Destination)
			.Include(x => x.Season)
			.Include(x => x.Sizes)
			.FirstOrDefaultAsync(x => x.Id == shoeId);

		createdShoe.Should().NotBeNull();
		createdShoe!.Name.Should().Be(command.Name);
		createdShoe!.Price.Should().Be(command.Price);
		createdShoe!.Brand!.Id.Should().Be(command.BrandId);
		createdShoe!.Destination!.Id.Should().Be(command.DestinationId);
		createdShoe!.Season!.Id.Should().Be(command.SeasonId);

		createdShoe!.Sizes.Should().NotBeNullOrEmpty();
		createdShoe!.Sizes!.Select(x => x.Id).ToHashSet()
			.Should().Equal(command.SizesIds);
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден файл изображения
	/// </summary>
	[Fact]
	public async Task PostShoeCommandHandler_ShouldThrow_WhenImageFileNotFound()
	{
		var command = new PostShoeCommand
		{
			ImageFileId = 404
		};

		var handler = new PostShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<File>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден Бренд обуви
	/// </summary>
	[Fact]
	public async Task PostShoeCommandHandler_ShouldThrow_WhenBrandNotFound()
	{
		var command = new PostShoeCommand
		{
			ImageFileId = _imageFile.Id,
			BrandId = 404
		};

		var handler = new PostShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Brand>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найдено Назначение обуви
	/// </summary>
	[Fact]
	public async Task PostShoeCommandHandler_ShouldThrow_WhenDestinationNotFound()
	{
		var command = new PostShoeCommand
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = 404,
		};

		var handler = new PostShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Destination>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найден Сезон обуви
	/// </summary>
	[Fact]
	public async Task PostShoeCommandHandler_ShouldThrow_WhenSeasonNotFound()
	{
		var command = new PostShoeCommand
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = _season.Id,
			SeasonId = 404,
		};

		var handler = new PostShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Season>>();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда указаны несуществующие размеры
	/// </summary>
	[Fact]
	public async Task PostShoeCommandHandler_ShouldThrow_WhenInvalidSizes()
	{
		var command = new PostShoeCommand
		{
			ImageFileId = _imageFile.Id,
			BrandId = _brand.Id,
			DestinationId = _season.Id,
			SeasonId = _season.Id,
			SizesIds = new HashSet<int> { 1, 404, 405 }
		};

		var handler = new PostShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<ApplicationExceptionBase>()
			.WithMessage("Указаны не существующие размеры с id = [404, 405]");
	}
}
