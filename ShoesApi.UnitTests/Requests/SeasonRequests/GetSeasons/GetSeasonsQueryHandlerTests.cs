using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.CQRS.Queries.Season.GetSeasons;
using ShoesApi.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.SeasonRequests.GetSeasons
{
	/// <summary>
	/// Тест для <see cref="GetSeasonsQueryHandler"/>
	/// </summary>
	public class GetSeasonsQueryHandlerTests : UnitTestBase
	{
		/// <summary>
		/// Должен вернуть брэнды, если они существуют в БД
		/// </summary>
		[Fact]
		public async Task GetSeasonsQueryHandler_ShouldReturnSeasons_WhenTheyExist()
		{
			var seasons = new List<Season>
			{
				new Season { Name = "Summer" },
				new Season { Name = "Fall" },
				new Season { Name = "Winter" },
			};

			using var context = CreateInMemoryContext(c =>
			{
				c.Seasons.AddRange(seasons);
				c.SaveChanges();
			});

			var handler = new GetSeasonsQueryHandler(context);
			var result = await handler.Handle(new GetSeasonsQuery(), default);

			result.TotalCount.Should().Be(3);
			result.Items.Should().NotBeNullOrEmpty();

			var firstSeason = seasons.First();
			var resultFirstSeason = result.Items!.First(x => x.Id == firstSeason.Id);

			resultFirstSeason.Name.Should().Be(firstSeason.Name);
		}
	}
}
