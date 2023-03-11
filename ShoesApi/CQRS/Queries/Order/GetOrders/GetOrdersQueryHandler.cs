using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Queries.Order.GetOrders
{
	/// <summary>
	/// Обработчик для <see cref="GetOrdersQuery"/>
	/// </summary>
	public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersResponse>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользовательских данных</param>
		public GetOrdersQueryHandler(ShoesDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <inheritdoc/>
		public async Task<GetOrdersResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			var query = _context.Orders
				.Where(x => x.User == user)
				.Select(x => new GetOrdersResponseItem()
				{
					Id = x.Id,
					OrderDate = x.OrderDate,
					Address = x.Address,
					Sum = x.Sum,
					Count = x.Count,
					OrderItems = x.OrderItems!.Select(i => new GetOrdersResponseItemOrderItem()
					{
						Id = i.Id,
						RuSize = i.Size!.RuSize,
						Shoe = new GetOrdersResponseItemOrderItemShoe()
						{
							Id = i.Shoe!.Id,
							Image = i.Shoe!.Image,
							Name = i.Shoe!.Name,
							Price = i.Shoe!.Price,
						}
					})
						.ToList()
				});

			var orders = await query.ToListAsync();
			var count = await query.CountAsync();

			return new GetOrdersResponse()
			{
				Items = orders,
				TotalCount = count,
			};
		}
	}
}
