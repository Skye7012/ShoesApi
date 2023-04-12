using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Sizes.Queries.GetSizes;
using ShoesApi.Domain.Entities;
using Xunit;

namespace ShoesApi.UnitTests.Requests.SizeRequests;

/// <summary>
/// Тест для <see cref="GetSizesQueryHandler"/>
/// </summary>
public class GetSizesQueryHandlerTests : UnitTestBase
{
	/// <summary>
	/// Должен вернуть брэнды, если они существуют в БД
	/// </summary>
	[Fact]
	public async Task GetSizesQueryHandler_ShouldReturnSizes_WhenTheyExist()
	{
		var sizes = new List<Size>
		{
			new Size
			{
				RuSize = 36,
			},
			new Size
			{
				RuSize = 40,
			},
			new Size
			{
				RuSize = 44,
			},
		};

		using var context = CreateInMemoryContext(c =>
		{
			c.Sizes.AddRange(sizes);
			c.SaveChanges();
		});

		var handler = new GetSizesQueryHandler(context);
		var result = await handler.Handle(new GetSizesQuery(), default);

		result.Should().NotBeNullOrEmpty();
		result.Should().Equal(new List<int> { 36, 40, 44 });
	}
}
