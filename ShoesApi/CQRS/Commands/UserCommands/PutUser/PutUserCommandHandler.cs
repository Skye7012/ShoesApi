using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Commands.UserCommands.PutUser
{
	/// <summary>
	/// Обработчик для <see cref="PutUserCommand"/>
	/// </summary>
	public class PutUserCommandHandler : IRequestHandler<PutUserCommand>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользователя</param>
		public PutUserCommandHandler(ShoesDbContext context, IUserService userService)
		{
			_context = context;
			_userService = userService;
		}

		/// <inheritdoc/>
		public async Task Handle(PutUserCommand request, CancellationToken cancellationToken)
		{
			var login = _userService.GetLogin();
			var user = await _context.Users
				.FirstOrDefaultAsync(x => x.Login == login)
				?? throw new Exception("User not found");

			if (request.Name != null)
				user.Name = request.Name;
			if (request.Surname != null)
				user.Surname = request.Surname;
			if (request.Phone != null)
				user.Phone = request.Phone;

			await _context.SaveChangesAsync();
		}
	}
}
