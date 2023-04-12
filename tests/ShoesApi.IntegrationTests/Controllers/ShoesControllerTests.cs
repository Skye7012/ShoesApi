using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using ShoesApi.Api.Controllers;
using ShoesApi.Application.Shoes.Queries.Common;
using ShoesApi.Infrastructure.Persistence;
using Xunit;

namespace ShoesApi.IntegrationTests.Controllers;

/// <summary>
/// Тесты для <see cref="ShoesController"/>
/// </summary>
public class ShoesControllerTests : IntegrationTestsBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="factory">Фабрика приложения</param>
	public ShoesControllerTests(IntegrationTestFactory<Program, ApplicationDbContext> factory) : base(factory)
	{
	}

	/// <summary>
	/// Возвращает коллекцию обуви, когда она существует
	/// </summary>
	[Fact]
	public async Task GetAsync_ReturnsShoes_WhenTheyExist()
	{
		var shoes = await Seeder.SeedShoesAsync();
		var shoe = shoes.First();

		var response = await Client.GetAsync("/Shoes").GetResponseAsyncAs<GetShoesResponse>();

		response.Should().NotBeNull();
		response.TotalCount.Should().Be(2);

		response.Items.Should().NotBeNull();

		var responseShoe = response.Items!.First();
		responseShoe.Name.Should().Be(shoe.Name);
		responseShoe.ImageFileId.Should().Be(shoe.ImageFileId);
		responseShoe.Price.Should().Be(shoe.Price);
		responseShoe.Brand.Id.Should().Be(shoe.BrandId);
		responseShoe.Destination.Id.Should().Be(shoe.DestinationId);
		responseShoe.Season.Id.Should().Be(shoe.SeasonId);
		responseShoe.RuSizes.Should().Equal(shoe.Sizes!.Select(x => x.RuSize));
	}

	/// <summary>
	/// Возвращает коллекцию обуви по идентификаторам, которые существуют
	/// </summary>
	[Fact]
	public async Task GetByIdsAsync_ReturnsShoes_ThatExists()
	{
		var shoes = await Seeder.SeedShoesAsync();
		var shoe = shoes.First(x => x.Id == 1);

		var parameters = new KeyValuePair<string, StringValues>[]
		{
			new ("ids", new StringValues(new string[] {"1", "42" })),
		};

		var uri = QueryHelpers.AddQueryString("/Shoes/GetByIds", parameters);
		var response = await Client.GetAsync(uri).GetResponseAsyncAs<GetShoesResponse>();

		response.Should().NotBeNull();
		response.TotalCount.Should().Be(1);

		response.Items.Should().NotBeNull();

		var responseShoe = Assert.Single(response.Items);
		responseShoe.Name.Should().Be(shoe.Name);
		responseShoe.ImageFileId.Should().Be(shoe.ImageFileId);
		responseShoe.Price.Should().Be(shoe.Price);
		responseShoe.Brand.Id.Should().Be(shoe.BrandId);
		responseShoe.Destination.Id.Should().Be(shoe.DestinationId);
		responseShoe.Season.Id.Should().Be(shoe.SeasonId);
		responseShoe.RuSizes.Should().Equal(shoe.Sizes!.Select(x => x.RuSize));
	}
}
