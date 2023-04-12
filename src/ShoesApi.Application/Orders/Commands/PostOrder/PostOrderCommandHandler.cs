using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;

namespace ShoesApi.Application.Orders.Commands.PostOrder;

/// <summary>
/// Обработчик для <see cref="PostOrderCommand"/>
/// </summary>
public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, int>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;
	private readonly IDateTimeProvider _dateTimeProvider;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользовательских данных</param>
	/// <param name="dateTimeProvider">Провайдер даты и времени</param>
	public PostOrderCommandHandler(IApplicationDbContext context, IUserService userService, IDateTimeProvider dateTimeProvider)
	{
		_context = context;
		_userService = userService;
		_dateTimeProvider = dateTimeProvider;
	}

	/// <inheritdoc/>
	public async Task<int> Handle(PostOrderCommand request, CancellationToken cancellationToken)
	{
		var login = _userService.GetLogin();
		var user = await _context.Users
			.FirstOrDefaultAsync(x => x.Login == login, cancellationToken)
			?? throw new UserNotFoundException(login);

		if (!IsOrderItemsUnique(request))
			throw new ValidationException("Комбинация ShoeId и RuSize должна быть уникальной");

		var shoes = await _context.Shoes
			.Where(x => request.OrderItems.Select(x => x.ShoeId).Contains(x.Id))
			.ToListAsync(cancellationToken);

		var sizes = await _context.Sizes
			.Where(x => request.OrderItems.Select(x => x.RuSize).Contains(x.RuSize))
			.ToListAsync(cancellationToken);

		var orderItems = request.OrderItems
			.Select(x => new OrderItem()
			{
				Shoe = shoes.FirstOrDefault(s => s.Id == x.ShoeId)
					?? throw new EntityNotFoundException<Shoe>(x.ShoeId),
				Size = sizes.FirstOrDefault(s => s.RuSize == x.RuSize)
					?? throw new EntityNotFoundException<Size>($"Не найден российский размер = {x.RuSize}"),
			})
			.ToList();

		var order = new Order()
		{
			OrderDate = _dateTimeProvider.UtcNow,
			Address = request.Address,
			Count = orderItems.Count,
			Sum = orderItems.Sum(x => x.Shoe!.Price),
			User = user,
			OrderItems = orderItems,
		};

		await _context.Orders.AddAsync(order, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return order.Id;
	}

	/// <summary>
	/// Являются ли элементы заказов уникальными
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <returns>Являются ли элементы заказов уникальными</returns>
	private static bool IsOrderItemsUnique(PostOrderCommand request)
	{
		return request.OrderItems
			.GroupBy(x => new { x.ShoeId, x.RuSize })
			.All(x => x.Count() == 1);
	}
}
