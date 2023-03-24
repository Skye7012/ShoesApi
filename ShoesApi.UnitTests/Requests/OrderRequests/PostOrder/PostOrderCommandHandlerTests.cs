﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.CQRS.Commands.OrderCommands.PostOrder;
using ShoesApi.Entities;
using ShoesApi.Entities.ShoeSimpleFilters;
using Xunit;

namespace ShoesApi.UnitTests.Requests.OrderRequests.PostOrder
{
	/// <summary>
	/// Тест для <see cref="PostOrderCommandHandler"/>
	/// </summary>
	public class PostOrderCommandHandlerTests : UnitTestBase
	{
		private readonly List<Size> _sizes;
		private readonly List<Shoe> _shoes;

		/// <summary>
		/// Конструктор
		/// </summary>
		public PostOrderCommandHandlerTests()
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
		/// Должен создать заказ, когда валидна команда
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task PostOrderCommand_ShouldCreateOrder_WhenCommandValid()
		{
			using var context = CreateInMemoryContext(c =>
			{
				c.AddRange(_shoes);
				c.SaveChanges();
			});

			var orderItemsToCreate = new List<PostOrderCommandOrderItem>
			{
				new PostOrderCommandOrderItem
				{
					ShoeId = _shoes[0].Id,
					RuSize = _sizes[0].RuSize,
				},
				new PostOrderCommandOrderItem
				{
					ShoeId = _shoes[1].Id,
					RuSize = _sizes[1].RuSize,
				},
			};

			var command = new PostOrderCommand
			{
				Address = "г. Москва, ул. Пушкина, дом Колотушкина",
				OrderItems = orderItemsToCreate,
			};

			var handler = new PostOrderCommandHandler(context, UserService, DateTimeProvider);
			var orderId = await handler.Handle(command, default);

			var createdOrder = await context.Orders
				.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == orderId);

			createdOrder.Should().NotBeNull();
			createdOrder!.Address.Should().Be(command.Address);
			createdOrder!.OrderDate.Should().Be(DateTimeProvider.UtcNow);
			createdOrder!.Sum.Should().Be(300);
			createdOrder!.Count.Should().Be(2);
			createdOrder!.User!.Id.Should().Be(UserService.AdminUser.Id);

			createdOrder.OrderItems.Should().NotBeNullOrEmpty();
			createdOrder.OrderItems.Should().HaveCount(2);

			createdOrder.OrderItems.Should().SatisfyRespectively(
				 first =>
				 {
					 first.ShoeId.Should().Be(_shoes[0].Id);
					 first.SizeId.Should().Be(_sizes[0].Id);
				 },
				second =>
				{
					second.ShoeId.Should().Be(_shoes[1].Id);
					second.SizeId.Should().Be(_sizes[1].Id);
				});
		}
	}
}