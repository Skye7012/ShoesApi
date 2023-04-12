using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Destinations.Queries.GetDestinations;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.DestinationRequests;

/// <summary>
/// Тест для <see cref="GetDestinationsQueryHandler"/>
/// </summary>
public class GetDestinationsQueryHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен вернуть брэнды, если они существуют в БД
	/// </summary>
	[Fact]
	public async Task GetDestinationsQueryHandler_ShouldReturnDestinations_WhenTheyExist()
	{
		var destinations = new List<Destination>
		{
			new Destination { Name = "Повседневная" },
			new Destination { Name = "Спортивная" },
			new Destination { Name = "Для прогулок" },
		};

		using var context = CreateInMemoryContext(c =>
		{
			c.Destinations.AddRange(destinations);
			c.SaveChanges();
		});

		var handler = new GetDestinationsQueryHandler(context);
		var result = await handler.Handle(new GetDestinationsQuery(), default);

		result.TotalCount.Should().Be(3);
		result.Items.Should().NotBeNullOrEmpty();

		var firstDestination = destinations.First();
		var resultFirstDestination = result.Items!.First(x => x.Id == firstDestination.Id);

		resultFirstDestination.Name.Should().Be(firstDestination.Name);
	}
}
