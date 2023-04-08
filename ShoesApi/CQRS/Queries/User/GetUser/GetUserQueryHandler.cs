using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesApi.Exceptions;
using ShoesApi.Services;

namespace ShoesApi.CQRS.Queries.User.GetUser
{
	/// <summary>
	/// Обработчик для of <see cref="GetUserQuery"/>
	/// </summary>
	public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
	{
		private readonly ShoesDbContext _context;
		private readonly IUserService _userService;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="context">Контекст БД</param>
		/// <param name="userService">Сервис пользовательских данных</param>
		public GetUserQueryHandler(ShoesDbContext context, IUserService userService)
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
}
