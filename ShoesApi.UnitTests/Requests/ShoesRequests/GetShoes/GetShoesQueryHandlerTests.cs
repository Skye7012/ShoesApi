using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Queries.Shoes.GetShoes;
using ShoesApi.Entities;
using ShoesApi.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.ShoeRequests.GetShoes
{
	/// <summary>
	/// Тест для <see cref="GetShoesQueryHandler"/>
	/// </summary>
	public class GetShoesQueryHandlerTests : UnitTestBase
	{
		private readonly List<Size> _sizes;
		private readonly List<Shoe> _shoes;

		/// <summary>
		/// Конструктор
		/// </summary>
		public GetShoesQueryHandlerTests()
		{
			_sizes = new List<Size>
			{
				new Size { RuSize = 40 },
				new Size { RuSize = 44 },
			};

			_shoes = new List<Shoe>
			{
				new Shoe
				{
					Name = "Sneakers",
					Image = "image.png",
					Price = 100,
					Brand = new Brand { Name = "Adidas" },
					Destination = new Destination { Name = "Sport" },
					Season = new Season { Name = "Summer" },
					Sizes = _sizes,
				},
				new Shoe
				{
					Name = "Boots",
					Image = "boots_image.png",
					Price = 200,
					Brand = new Brand { Name = "Dr. Martens" },
					Destination = new Destination { Name = "Everyday" },
					Season = new Season { Name = "Winter" },
					Sizes = _sizes,
				}
			};
		}

		/// <summary>
		/// Должен вернуть брэнды, если они существуют в БД
		/// </summary>
		[Fact]
		public async Task GetShoesQueryHandler_ShouldReturnShoes_WhenTheyExist()
		{
			using var context = CreateInMemoryContext(c =>
			{
				c.Shoes.AddRange(_shoes);
				c.SaveChanges();
			});

			var handler = new GetShoesQueryHandler(context);
			var result = await handler.Handle(new GetShoesQuery(), default);

			result.TotalCount.Should().Be(2);
			result.Items.Should().NotBeNullOrEmpty();

			var firstShoe = _shoes.First();
			var resultFirstShoe = result.Items!.First(x => x.Id == firstShoe.Id);

			resultFirstShoe.Name.Should().Be(firstShoe.Name);
			resultFirstShoe.Image.Should().Be(firstShoe.Image);
			resultFirstShoe.Price.Should().Be(firstShoe.Price);

			resultFirstShoe.Brand.Id.Should().Be(firstShoe.Brand!.Id);
			resultFirstShoe.Brand.Name.Should().Be(firstShoe.Brand!.Name);
			resultFirstShoe.Season.Id.Should().Be(firstShoe.Season!.Id);
			resultFirstShoe.Season.Name.Should().Be(firstShoe.Season!.Name);
			resultFirstShoe.Destination.Id.Should().Be(firstShoe.Destination!.Id);
			resultFirstShoe.Destination.Name.Should().Be(firstShoe.Destination!.Name);
			
			resultFirstShoe.RuSizes.Should().Equal(firstShoe.Sizes!.Select(x => x.RuSize).ToList());
		}
	}
}
