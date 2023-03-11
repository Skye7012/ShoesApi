using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.CQRS.Commands.UserCommands.DeleteUser;
using ShoesApi.CQRS.Commands.UserCommands.PutUser;
using ShoesApi.CQRS.Commands.UserCommands.SignInUser;
using ShoesApi.CQRS.Commands.UserCommands.SignUpUser;
using ShoesApi.CQRS.Queries.User.GetUser;
using ShoesApi.Entities;

namespace ShoesApi.Controllers
{
	/// <summary>
	/// Контроллер для <see cref="User"/>
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="mediator">Медиатор</param>
		public UserController(IMediator mediator)
			=> _mediator = mediator;

		/// <summary>
		/// Получить данные о пользователе
		/// </summary>
		/// <returns>Данные о пользователе</returns>
		[HttpGet]
		[Authorize]
		public async Task<GetUserResponse> Get()
			=> await _mediator.Send(new GetUserQuery());

		/// <summary>
		/// Зарегистрироваться
		/// </summary>
		/// <param name="request">Запрос</param>
		/// <returns>Идентификатор созданного пользователя</returns>
		[HttpPost("SignUp")]
		public async Task<SignUpUserResponse> SignUp(SignUpUserCommand request)
			=> await _mediator.Send(request);

		/// <summary>
		/// Авторизоваться
		/// </summary>
		/// <param name="request">Запрос</param>
		/// <returns>Authorization token</returns>
		[HttpPost("SignIn")]
		public async Task<ActionResult<SignInUserResponse>> SignIn(SignInUserCommand request)
		{
			// TODO: реализовать обработку ошибок
			try
			{
				return Ok(await _mediator.Send(request));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Обновить данные о пользователе
		/// </summary>
		/// <param name="request">Запрос</param>
		[HttpPut]
		[Authorize]
		public async Task Put(PutUserCommand request)
			=> await _mediator.Send(request);

		/// <summary>
		/// Удалить пользователя
		/// </summary>
		[HttpDelete]
		[Authorize]
		public async Task Delete()
			=> await _mediator.Send(new DeleteUserCommand());
	}
}
