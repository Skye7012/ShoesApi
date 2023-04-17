using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using ShoesApi.Api.Controllers;
using ShoesApi.Contracts.Requests.Shoes.Common;
using ShoesApi.Contracts.Requests.Shoes.PostShoe;
using ShoesApi.Contracts.Requests.Shoes.PutShoe;
using ShoesApi.Domain.Entities;
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

	/// <summary>
	/// Создает обувь при валидном запросе
	/// </summary>
	[Fact]
	public async Task PostAsync_CreateShoe_WhenRequestValid()
	{
		var sizes = await DbContext.Sizes.ToListAsync();
		var brand = await DbContext.Brands.FirstOrDefaultAsync();
		var destination = await DbContext.Destinations.FirstOrDefaultAsync();
		var season = await DbContext.Seasons.FirstOrDefaultAsync();

		var imageFile = new File("test.test");
		await DbContext.Files.AddAsync(imageFile);
		await DbContext.SaveChangesAsync();

		var request = new PostShoeRequest
		{
			Name = "Sneakers",
			ImageFileId = imageFile.Id,
			Price = 100,
			BrandId = brand!.Id,
			DestinationId = destination!.Id,
			SeasonId = season!.Id,
			SizesIds = sizes.Select(x => x.Id).ToHashSet(),
		};

		var createShoeId = await Client
			.PostAsJsonAsync("/Shoes", request)
			.GetResponseAsyncAs<int>();

		var createdShoe = await DbContext.Shoes
			.Include(x => x.Brand)
			.Include(x => x.Destination)
			.Include(x => x.Season)
			.Include(x => x.Sizes)
			.FirstOrDefaultAsync(x => x.Id == createShoeId);

		createdShoe.Should().NotBeNull();
		createdShoe!.Name.Should().Be(request.Name);
		createdShoe!.Price.Should().Be(request.Price);
		createdShoe!.Brand!.Id.Should().Be(request.BrandId);
		createdShoe!.Destination!.Id.Should().Be(request.DestinationId);
		createdShoe!.Season!.Id.Should().Be(request.SeasonId);

		createdShoe!.Sizes.Should().NotBeNullOrEmpty();
		createdShoe!.Sizes!.Select(x => x.Id).ToHashSet()
			.Should().Equal(request.SizesIds);
	}

	/// <summary>
	/// Обновляет обувь при валидном запросе
	/// </summary>
	[Fact]
	public async Task PutAsync_UpdateShoe_WhenRequestValid()
	{
		var shoes = await Seeder.SeedShoesAsync();
		var shoe = shoes.First();
		var sizes = await DbContext.Sizes.ToListAsync();

		var brand = await DbContext.Brands
			.FirstOrDefaultAsync(x => x.Id != shoe.BrandId);

		var destination = await DbContext.Destinations
			.FirstOrDefaultAsync(x => x.Id != shoe.DestinationId);

		var season = await DbContext.Seasons
			.FirstOrDefaultAsync(x => x.Id != shoe.SeasonId);

		var imageFile = new File("new.test");
		await DbContext.Files.AddAsync(imageFile);
		await DbContext.SaveChangesAsync();

		var request = new PutShoeRequest
		{
			Name = "NewSneakers",
			ImageFileId = imageFile.Id,
			Price = 3000,
			BrandId = brand!.Id,
			DestinationId = destination!.Id,
			SeasonId = season!.Id,
			SizesIds = sizes.GetRange(2, 2).Select(x => x.Id).ToHashSet(),
		};

		var putResponse = await Client.PutAsJsonAsync($"/Shoes/{shoe.Id}", request);

		DbContext.Instance.ChangeTracker.Clear();

		putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

		var updatedShoe = await DbContext.Shoes
			.Include(x => x.Brand)
			.Include(x => x.Destination)
			.Include(x => x.Season)
			.Include(x => x.Sizes)
			.FirstOrDefaultAsync(x => x.Id == shoe.Id);

		updatedShoe.Should().NotBeNull();
		updatedShoe!.Name.Should().Be(request.Name);
		updatedShoe!.Price.Should().Be(request.Price);
		updatedShoe!.Brand!.Id.Should().Be(request.BrandId);
		updatedShoe!.Destination!.Id.Should().Be(request.DestinationId);
		updatedShoe!.Season!.Id.Should().Be(request.SeasonId);

		updatedShoe!.Sizes.Should().NotBeNullOrEmpty();
		updatedShoe!.Sizes!.Select(x => x.Id).ToHashSet()
			.Should().Equal(request.SizesIds);
	}

	/// <summary>
	/// Удаляет обувь при валидном запросе
	/// </summary>
	[Fact]
	public async Task DeleteAsync_RemoveShoe_WhenRequestValid()
	{
		var shoes = await Seeder.SeedShoesAsync();
		var shoe = shoes.First();

		var deleteResponse = await Client.DeleteAsync($"/Shoes/{shoe.Id}");

		deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

		var deletedShoe = await DbContext.Shoes
			.FirstOrDefaultAsync(x => x.Id == shoe.Id);

		deletedShoe.Should().BeNull();
	}
}
