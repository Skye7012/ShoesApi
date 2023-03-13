using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Entities;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Commands.OrderCommands.PostOrder
{
	/// <summary>
	/// Обработчик для <see cref="PostOrderCommand"/>
	/// </summary>
	public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, int>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользовательских данных</param>
		public PostOrderCommandHandler(ShoesDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <inheritdoc/>
		public async Task<int> Handle(PostOrderCommand request, CancellationToken cancellationToken)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			if (!IsOrderItemsUnique(request))
				throw new Exception("ShoeId and RuSize combination must be unique");

			var shoes = await _context.Shoes
				.Where(x => request.OrderItems.Select(x => x.ShoeId).Contains(x.Id))
				.ToListAsync();

			var sizes = await _context.Sizes
				.Where(x => request.OrderItems.Select(x => x.RuSize).Contains(x.RuSize))
				.ToListAsync();

			var orderItems = request.OrderItems
				.Select(x => new OrderItem()
				{
					Shoe = shoes.FirstOrDefault(s => s.Id == x.ShoeId)
						?? throw new Exception($"Not found shoe with id {x.ShoeId}"),
					Size = sizes.FirstOrDefault(s => s.RuSize == x.RuSize)
						?? throw new Exception($"Not found RuSuze {x.RuSize}"),
				})
				.ToList();

			var order = new Order()
			{
				OrderDate = DateTime.UtcNow,
				Address = request.Address,
				Count = orderItems.Count(),
				Sum = orderItems.Sum(x => x.Shoe!.Price),
				User = user,
				OrderItems = orderItems,
			};

			await _context.AddAsync(order);
			await _context.SaveChangesAsync();

			return order.Id;
		}

		/// <summary>
		/// Являются ли элементы заказов уникальными
		/// </summary>
		/// <param name="request">Запрос</param>
		/// <returns>Являются ли элементы заказов уникальными</returns>
		private bool IsOrderItemsUnique(PostOrderCommand request)
		{
			return request.OrderItems
				.GroupBy(x => new { x.ShoeId, x.RuSize })
				.All(x => x.Count() == 1);
		}
	}
}
