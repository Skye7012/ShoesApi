using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;

namespace ShoesApi.Application.Users.Commands.PutUser;

/// <summary>
/// Обработчик для <see cref="PutUserCommand"/>
/// </summary>
public class PutUserCommandHandler : IRequestHandler<PutUserCommand>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользователя</param>
	public PutUserCommandHandler(IApplicationDbContext context, IUserService userService)
	{
		_context = context;
		_userService = userService;
	}

	/// <inheritdoc/>
	public async Task Handle(PutUserCommand request, CancellationToken cancellationToken)
	{
		var login = _userService.GetLogin();
		var user = await _context.Users
			.FirstOrDefaultAsync(x => x.Login == login, cancellationToken)
			?? throw new UserNotFoundException(login);

		if (request.Name != null)
			user.Name = request.Name;
		if (request.Surname != null)
			user.Surname = request.Surname;
		if (request.Phone != null)
			user.Phone = request.Phone;

		await _context.SaveChangesAsync(cancellationToken);
	}
}
