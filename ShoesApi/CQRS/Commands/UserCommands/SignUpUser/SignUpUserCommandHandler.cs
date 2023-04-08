using MediatR;
using ShoesApi.Entities;
using ShoesApi.Exceptions;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Commands.UserCommands.SignUpUser
{
	/// <summary>
	/// Обработчик для <see cref="SignUpUserCommand"/>
	/// </summary>
	public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, SignUpUserResponse>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользователя</param>
		public SignUpUserCommandHandler(ShoesDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <inheritdoc/>
		public async Task<SignUpUserResponse> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
		{
			var isLoginUnique = _context.Users.All(x => x.Login != request.Login);
			if (!isLoginUnique)
				throw new ValidationException("Пользователь с таким логином уже существует");

			_userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = new User(
				login: request.Login,
				passwordHash: passwordHash,
				passwordSalt: passwordSalt,
				name: request.Name,
				surname: request.Surname,
				phone: request.Phone);

			await _context.AddAsync(user, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return new SignUpUserResponse()
			{
				UserId = user.Id,
			};
		}
	}
}
