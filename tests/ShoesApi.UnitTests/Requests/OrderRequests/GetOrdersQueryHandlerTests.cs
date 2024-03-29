using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Orders.Queries.GetOrders;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.OrderRequests;

/// <summary>
/// Тест для <see cref="GetOrdersQueryHandler"/>
/// </summary>
public class GetOrdersQueryHandlerTests : UnitTestBase
{
	private readonly File _imageFile;
	private readonly List<Size> _sizes;
	private readonly Order _order;

	/// <summary>
	/// Конструктор
	/// </summary>
	public GetOrdersQueryHandlerTests()
	{
		_imageFile = new File("test");

		_sizes = new List<Size>
		{
			new Size { RuSize = 40 },
			new Size { RuSize = 44 },
		};

		_order = new Order()
		{
			OrderDate = DateTimeProvider.UtcNow,
			Address = "г. Москва, ул. Пушкина, дом Колотушкина",
			Sum = 300,
			Count = 2,
			User = UserService.AdminUser,
			OrderItems = new List<OrderItem>
			{
				new OrderItem
				{
					Shoe =  new Shoe
					{
						Name = "Sneakers",
						ImageFile = _imageFile,
						Price = 100,
						Brand = new Brand { Name = "Adidas" },
						Destination = new Destination { Name = "Sport"},
						Season = new Season { Name = "Summer"},
						Sizes = _sizes,
					},
					Size = _sizes[0],
				},
				new OrderItem
				{
					Shoe = new Shoe
					{
						Name = "Boots",
						ImageFile = _imageFile,
						Price = 200,
						Brand = new Brand { Name = "Dr. Martens" },
						Destination = new Destination { Name = "Everyday" },
						Season = new Season { Name = "Winter" },
						Sizes = _sizes,
					},
					Size = _sizes[1],
				}
			}
		};
	}

	/// <summary>
	/// Должен вернуть брэнды, если они существуют в БД
	/// </summary>
	[Fact]
	public async Task GetOrdersQueryHandler_ShouldReturnOrders_WhenTheyExist()
	{
		using var context = CreateInMemoryContext(c =>
		{
			c.Orders.Add(_order);
			c.SaveChanges();
		});

		var handler = new GetOrdersQueryHandler(context, UserService);
		var result = await handler.Handle(new GetOrdersQuery(), default);

		result.TotalCount.Should().Be(1);
		result.Items.Should().NotBeNullOrEmpty();

		var resultOrder = Assert.Single(result.Items!);

		resultOrder.Id.Should().Be(_order.Id);
		resultOrder.OrderDate.Should().Be(_order.OrderDate);
		resultOrder.Sum.Should().Be(_order.Sum);
		resultOrder.Count.Should().Be(_order.Count);
		resultOrder.Address.Should().Be(_order.Address);

		resultOrder.OrderItems.Should().NotBeNullOrEmpty();
		resultOrder.OrderItems.Should().HaveCount(2);

		var firstOrderItem = _order.OrderItems!.First();
		var resultFirstOrderItem = resultOrder.OrderItems!.First();

		resultFirstOrderItem.Id.Should().Be(firstOrderItem.Id);
		resultFirstOrderItem.RuSize.Should().Be(firstOrderItem.Size!.RuSize);
		resultFirstOrderItem.Shoe.Id.Should().Be(firstOrderItem.Shoe!.Id);
		resultFirstOrderItem.Shoe.ImageFileId.Should().Be(firstOrderItem.Shoe!.ImageFileId);
		resultFirstOrderItem.Shoe.Name.Should().Be(firstOrderItem.Shoe!.Name);
		resultFirstOrderItem.Shoe.Price.Should().Be(firstOrderItem.Shoe!.Price);
	}

	/// <summary>
	/// Должен выкинуть ошибку, когда пользователь не найден
	/// </summary>
	[Fact]
	public async Task GetOrdersQueryHandler_ShouldThrow_WhenUserNotFound()
	{
		using var context = CreateInMemoryContext(x =>
		{
			x.Users.Remove(UserService.AdminUser);
			x.SaveChanges();
		});

		var handler = new GetOrdersQueryHandler(context, UserService);
		var handle = async () => await handler.Handle(new GetOrdersQuery(), default);

		await handle.Should()
			.ThrowAsync<UserNotFoundException>();
	}
}
