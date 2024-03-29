using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Contracts.Requests.Orders.GetOrders;

namespace ShoesApi.Application.Orders.Queries.GetOrders;

/// <summary>
/// Обработчик для <see cref="GetOrdersQuery"/>
/// </summary>
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользовательских данных</param>
	public GetOrdersQueryHandler(IApplicationDbContext context, IUserService userService)
	{
		_context = context;
		_userService = userService;
	}

	/// <inheritdoc/>
	public async Task<GetOrdersResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		var login = _userService.GetLogin();
		var user = await _context.Users
			.FirstOrDefaultAsync(x => x.Login == login, cancellationToken)
			?? throw new UserNotFoundException(login);

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
						ImageFileId = i.Shoe!.ImageFileId,
						Name = i.Shoe!.Name,
						Price = i.Shoe!.Price,
					}
				})
				.ToList()
			});

		var orders = await query.ToListAsync(cancellationToken);
		var count = await query.CountAsync(cancellationToken);

		return new GetOrdersResponse()
		{
			Items = orders,
			TotalCount = count,
		};
	}
}
