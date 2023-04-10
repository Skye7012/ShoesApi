using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ShoesApi.CQRS.Commands.OrderCommands.PostOrder;
using ShoesApi.Entities;
using ShoesApi.Entities.ShoeSimpleFilters;
using ShoesApi.Exceptions;
using Xunit;

namespace ShoesApi.UnitTests.Requests.OrderRequests
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
			var imageFile = new File("test");

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
					ImageFile = imageFile,
					Price = 100,
					Brand = new Brand { Name = "Adidas" },
					Destination = new Destination { Name = "Sport" },
					Season = new Season { Name = "Summer" },
					Sizes = _sizes,
				},
				new Shoe
				{
					Name = "Boots",
					ImageFile = imageFile,
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

		/// <summary>
		/// Должен выкинуть ошибку, когда пользователь не найден
		/// </summary>
		[Fact]
		public async Task PostOrderCommandHandler_ShouldThrow_WhenUserNotFound()
		{
			using var context = CreateInMemoryContext(x =>
			{
				x.Users.Remove(UserService.AdminUser);
				x.SaveChanges();
			});

			var handler = new PostOrderCommandHandler(context, UserService, DateTimeProvider);
			var handle = async () => await handler.Handle(new PostOrderCommand(), default);

			await handle.Should()
				.ThrowAsync<UserNotFoundException>();
		}

		/// <summary>
		/// Должен выкинуть ошибку, когда комбинация ShoeId и RuSize не уникальна
		/// </summary>
		[Fact]
		public async Task PostOrderCommandHandler_ShouldThrow_WhenShoeIdAndRuSizeCombinationNotUnique()
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
					ShoeId = _shoes[0].Id,
					RuSize = _sizes[0].RuSize,
				},
			};

			var command = new PostOrderCommand
			{
				Address = "г. Москва, ул. Пушкина, дом Колотушкина",
				OrderItems = orderItemsToCreate,
			};

			var handler = new PostOrderCommandHandler(context, UserService, DateTimeProvider);
			var handle = async () => await handler.Handle(command, default);

			await handle.Should()
				.ThrowAsync<ValidationException>()
				.WithMessage("Комбинация ShoeId и RuSize должна быть уникальной");
		}

		/// <summary>
		/// Должен выкинуть ошибку, когда передан не существующий ShoeId
		/// </summary>
		[Fact]
		public async Task PostOrderCommandHandler_ShouldThrow_WhenGivenNotExistingShoeId()
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
					ShoeId = 404,
					RuSize = _sizes[0].RuSize,
				},
			};

			var command = new PostOrderCommand
			{
				Address = "г. Москва, ул. Пушкина, дом Колотушкина",
				OrderItems = orderItemsToCreate,
			};

			var handler = new PostOrderCommandHandler(context, UserService, DateTimeProvider);
			var handle = async () => await handler.Handle(command, default);

			await handle.Should()
				.ThrowAsync<EntityNotFoundException<Shoe>>()
				.WithMessage($"Не удалось найти сущность '{nameof(Shoe)}' по id = '{404}'");
		}

		/// <summary>
		/// Должен выкинуть ошибку, когда передан не существующий RuSize
		/// </summary>
		[Fact]
		public async Task PostOrderCommandHandler_ShouldThrow_WhenGivenNotExistingRuSize()
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
					RuSize = 404,
				},
			};

			var command = new PostOrderCommand
			{
				Address = "г. Москва, ул. Пушкина, дом Колотушкина",
				OrderItems = orderItemsToCreate,
			};

			var handler = new PostOrderCommandHandler(context, UserService, DateTimeProvider);
			var handle = async () => await handler.Handle(command, default);

			await handle.Should()
				.ThrowAsync<EntityNotFoundException<Size>>()
				.WithMessage($"Не найден российский размер = {404}");
		}
	}
}
