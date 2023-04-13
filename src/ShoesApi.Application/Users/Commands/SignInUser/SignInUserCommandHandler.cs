using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Exceptions;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Contracts.Requests.Users.SignInUser;

namespace ShoesApi.Application.Users.Commands.SignInUser;

/// <summary>
/// Обработчик для <see cref="SignInUserCommand"/>
/// </summary>
public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserResponse>
{
	private readonly IApplicationDbContext _context;
	private readonly IUserService _userService;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="context">Контекст БД</param>
	/// <param name="userService">Сервис пользователя</param>
	public SignInUserCommandHandler(IApplicationDbContext context, IUserService userService)
	{
		_context = context;
		_userService = userService;
	}

	/// <inheritdoc/>
	public async Task<SignInUserResponse> Handle(SignInUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken)
			?? throw new UserNotFoundException(request.Login);

		if (!_userService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
		{
			throw new ValidationException("Неправильный пароль");
		}

		string token = _userService.CreateToken(user);

		return new SignInUserResponse()
		{
			Token = token,
		};
	}
}
