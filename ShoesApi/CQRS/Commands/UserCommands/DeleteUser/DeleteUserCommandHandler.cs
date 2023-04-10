using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Exceptions;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Commands.UserCommands.DeleteUser
{
	/// <summary>
	/// Обработчик для <see cref="DeleteUserCommand"/>
	/// </summary>
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользователя</param>
		public DeleteUserCommandHandler(ShoesDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <inheritdoc/>
		public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new UserNotFoundException(login);

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}
	}
}
