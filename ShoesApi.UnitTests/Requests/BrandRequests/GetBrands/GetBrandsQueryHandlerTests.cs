using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Queries.Brand.GetBrands;
using ShoesApi.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.BrandRequests.GetBrands
{
	/// <summary>
	/// Тест для <see cref="GetBrandsQueryHandler"/>
	/// </summary>
	public class GetBrandsQueryHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен вернуть брэнды, если они существуют в БД
		/// </summary>
		[Fact]
		public async Task GetBrandsQueryHandler_ShouldReturnBrands_WhenTheyExist()
		{
			var brands = new List<Brand>
			{
				new Brand { Name = "Nike" },
				new Brand { Name = "Adidas" },
				new Brand { Name = "Puma" }
			};

			using var context = CreateInMemoryContext(c =>
			{
				c.Brands.AddRange(brands);
				c.SaveChanges();
			});

			var handler = new GetBrandsQueryHandler(context);
			var result = await handler.Handle(new GetBrandsQuery(), default);

			result.TotalCount.Should().Be(3);
			result.Items.Should().NotBeNullOrEmpty();

			var firstBrand = brands.First();
			var resultFirstBrand = result.Items!.First(x => x.Id == firstBrand.Id);

			resultFirstBrand.Name.Should().Be(firstBrand.Name);
		}
	}
}
