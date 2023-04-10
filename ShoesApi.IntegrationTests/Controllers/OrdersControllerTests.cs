using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.CQRS.Commands.OrderCommands.PostOrder;
using ShoesApi.CQRS.Queries.Order.GetOrders;
using ShoesApi.Entities;
using Xunit;

namespace ShoesApi.IntegrationTests.Controllers
{
	/// <summary>
	/// Тесты для <see cref="OrdersController"/>
	/// </summary>
	public class OrdersControllerTests : IntegrationTestsBase
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="factory">Фабрика приложения</param>
		public OrdersControllerTests(IntegrationTestFactory<Program, ShoesDbContext> factory) : base(factory)
		{
		}

		/// <summary>
		/// Возвращает коллекцию заказов пользователя, когда она существует
		/// </summary>
		[Fact]
		public async Task GetAsync_ReturnsOrders_WhenTheyExist()
		{
			var shoes = await Seeder.SeedShoesAsync();
			var sizes = await DbContext.Sizes.ToListAsync();

			var orders = new List<Order>
			{
				new Order
				{
					Address = "г. Москва, ул. Пушкина, дом Колотушкина",
					Count = 2,
					OrderDate = DateTimeProvider.UtcNow,
					Sum = 300,
					User = Seeder.AdminUser,
					OrderItems = new List<OrderItem>
					{
						new OrderItem
						{
							Shoe = shoes[0],
							Size = sizes[0],
						},
						new OrderItem
						{
							Shoe = shoes[1],
							Size = sizes[1],
						},
					}
				},
				new Order
				{
					Address = "г. Москва, ул. Пушкина, дом Колотушкина",
					Count = 1,
					OrderDate = DateTimeProvider.UtcNow,
					Sum = 200,
					User = Seeder.AdminUser,
					OrderItems = new List<OrderItem>
					{
						new OrderItem
						{
							Shoe = shoes[1],
							Size = sizes[1],
						}
					}
				},
			};

			await DbContext.Orders.AddRangeAsync(orders);
			await DbContext.SaveChangesAsync();

			Authenticate();
			var response = await Client.GetAsync("/Orders").GetResponseAsyncAs<GetOrdersResponse>();

			response.Should().NotBeNull();
			response.TotalCount.Should().Be(2);

			response.Items.Should().NotBeNull();

			var order = orders.First();
			var responseOrder = response.Items!.First();
			responseOrder.Address.Should().Be(order.Address);
			responseOrder.Count.Should().Be(order.Count);
			responseOrder.Sum.Should().Be(order.Sum);
			responseOrder.OrderDate.Should().Be(order.OrderDate);

			responseOrder.OrderItems.Should().NotBeNullOrEmpty();
			responseOrder.OrderItems!.Count.Should().Be(2);

			var orderItem = order.OrderItems!.First();
			var responseOrderItem = responseOrder.OrderItems!.First();
			responseOrderItem.RuSize.Should().Be(orderItem.Size!.RuSize);
			responseOrderItem.Shoe.ImageFileId.Should().Be(orderItem.Shoe!.ImageFileId);
			responseOrderItem.Shoe.Name.Should().Be(orderItem.Shoe!.Name);
			responseOrderItem.Shoe.Price.Should().Be(orderItem.Shoe!.Price);
		}

		/// <summary>
		/// Создает заказ при валидном запросе
		/// </summary>
		[Fact]
		public async Task PostAsync_CreateOrder_WhenRequestValid()
		{
			var shoes = await Seeder.SeedShoesAsync();
			var sizes = await DbContext.Sizes.ToListAsync();

			Authenticate();

			var createOrderId = await Client
				.PostAsJsonAsync("/Orders", new PostOrderCommand
				{
					Address = "г. Москва, ул. Пушкина, дом Колотушкина",
					OrderItems = new List<PostOrderCommandOrderItem>
					{
						new PostOrderCommandOrderItem
						{
							RuSize = sizes[0].RuSize,
							ShoeId = shoes[0].Id,
						},
						new PostOrderCommandOrderItem
						{
							RuSize = sizes[1].RuSize,
							ShoeId = shoes[1].Id,
						}
					}
				})
					.GetResponseAsyncAs<int>();

			var createdOrder = await DbContext.Orders
				.Include(x => x.OrderItems)
				.FirstOrDefaultAsync(x => x.Id == createOrderId);

			createdOrder.Should().NotBeNull();

			createdOrder!.OrderItems.Should().NotBeNullOrEmpty();
			createdOrder.Address.Should().Be("г. Москва, ул. Пушкина, дом Колотушкина");
			createdOrder.Sum.Should().Be(300);
			createdOrder.Count.Should().Be(2);
			createdOrder!.OrderItems!.Count.Should().Be(2);
			createdOrder.OrderDate.Should().Be(DateTimeProvider.UtcNow);
			createdOrder.User.Should().Be(Seeder.AdminUser);


			var createdOrderItem = createdOrder!.OrderItems!.First();

			createdOrderItem.Order.Should().Be(createdOrder);
			createdOrderItem.Shoe.Should().Be(shoes[0]);
			createdOrderItem.Size.Should().Be(sizes[0]);
		}
	}
}
