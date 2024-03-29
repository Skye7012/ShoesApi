using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Contracts.Requests.Users.GetUser;

namespace ShoesApi.Application.Users.Queries.GetUser;

/// <summary>
/// Обработчик для of <see cref="GetUserQuery"/>
/// </summary>
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользовательских данных</param>
	public GetUserQueryHandler(IApplicationDbContext context, IUserService userService)
	{
		_context = context;
		_userService = userService;
	}

	/// <inheritdoc/>
	public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var login = _userService.GetLogin();
		var user = await _context.Users
			.FirstOrDefaultAsync(x => x.Login == login, cancellationToken)
			?? throw new UserNotFoundException(login);

		return new GetUserResponse()
		{
			Login = login,
			Name = user.Name,
			Surname = user.Surname,
			Phone = user.Phone,
		};
	}
}
