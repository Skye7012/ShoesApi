using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Application.Users.Commands.DeleteUser;

/// <summary>
/// Обработчик для <see cref="DeleteUserCommand"/>
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользователя</param>
	public DeleteUserCommandHandler(IApplicationDbContext context, IUserService userService)
	{
		_context = context;
		_userService = userService;
	}

	/// <inheritdoc/>
	public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var login = _userService.GetLogin();
		var user = await _context.Users
			.Include(x => x.Orders!)
				.ThenInclude(x => x.OrderItems)
			.FirstOrDefaultAsync(x => x.Login == login, cancellationToken)
			?? throw new UserNotFoundException(login);

		_context.Users.Remove(user);
		await _context.SaveChangesAsync(cancellationToken);
	}
}
