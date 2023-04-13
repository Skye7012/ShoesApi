using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Application.Shoes.Commands.DeleteShoe;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.ShoesRequests;

/// <summary>
/// Тест для <see cref="DeleteShoeCommandHandler"/>
/// </summary>
public class DeleteShoeCommandHandlerTests : UnitTestBase
{
	private readonly Shoe _shoe;
	private readonly IApplicationDbContext _context;

	/// <summary>
	/// Конструктор
	/// </summary>
	public DeleteShoeCommandHandlerTests()
	{
		_shoe = new Shoe
		{
			Name = "Sneakers",
			ImageFile = new File("test"),
			Price = 100,
			Brand = new Brand { Name = "Adidas" },
			Destination = new Destination { Name = "Sport" },
			Season = new Season { Name = "Summer" },
			Sizes = new List<Size>
			{
				new Size { RuSize = 40 },
				new Size { RuSize = 44 },
			},
		};

		_context = CreateInMemoryContext(c =>
		{
			c.Shoes.Add(_shoe);
			c.SaveChanges();
		});
	}

	/// <summary>
	/// Должен удалить обувь, когда валиден запрос
	/// </summary>
	[Fact]
	public async Task DeleteShoeCommand_ShouldCreateShoe_WhenRequestValid()
	{
		var command = new DeleteShoeCommand(_shoe.Id);

		var handler = new DeleteShoeCommandHandler(_context);
		await handler.Handle(command, default);

		var deletedShoe = await _context.Shoes
			.FirstOrDefaultAsync(x => x.Id == _shoe.Id);

		deletedShoe.Should().BeNull();
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда не найдена обувь
	/// </summary>
	[Fact]
	public async Task DeleteShoeCommandHandler_ShouldThrow_WhenShoeNotFound()
	{
		var command = new DeleteShoeCommand(404);

		var handler = new DeleteShoeCommandHandler(_context);
		var handle = async () => await handler.Handle(command, default);

		await handle.Should()
			.ThrowAsync<EntityNotFoundException<Shoe>>();
	}
}
