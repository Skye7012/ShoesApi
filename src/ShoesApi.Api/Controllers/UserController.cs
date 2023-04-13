using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Users.Commands.DeleteUser;
using ShoesApi.Application.Users.Commands.PutUser;
using ShoesApi.Application.Users.Commands.SignInUser;
using ShoesApi.Application.Users.Commands.SignUpUser;
using ShoesApi.Application.Users.Queries.GetUser;
using ShoesApi.Contracts.Requests.Users.GetUser;
using ShoesApi.Contracts.Requests.Users.PutUser;
using ShoesApi.Contracts.Requests.Users.SignInUser;
using ShoesApi.Contracts.Requests.Users.SignUpUser;

namespace ShoesApi.Api.Controllers;

/// <summary>
/// Контроллер для Пользователей
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
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Данные о пользователе</returns>
	[HttpGet]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<GetUserResponse> GetAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new GetUserQuery(), cancellationToken);

	/// <summary>
	/// Зарегистрироваться
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Идентификатор созданного пользователя</returns>
	[HttpPost("SignUp")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<SignUpUserResponse>> SignUpAsync(
		SignUpUserRequest request,
		CancellationToken cancellationToken)
			=> CreatedAtAction(
				nameof(GetAsync),
				await _mediator.Send(
					new SignUpUserCommand
					{
						Login = request.Login,
						Name = request.Name,
						Password = request.Password,
						Surname = request.Surname,
						Phone = request.Phone,
					},
					cancellationToken));

	/// <summary>
	/// Авторизоваться
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Authorization token</returns>
	[HttpPost("SignIn")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<SignInUserResponse> SignInAsync(
		SignInUserRequest request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(
			new SignInUserCommand
			{
				Login = request.Login,
				Password = request.Password,
			},
			cancellationToken);

	/// <summary>
	/// Обновить данные о пользователе
	/// </summary>
	/// <param name="request">Запрос</param>
	/// <param name="cancellationToken">Токен отмены</param>
	[HttpPut]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task PutAsync(
		PutUserRequest request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(
			new PutUserCommand
			{
				Name = request.Name,
				Phone = request.Phone,
				Surname = request.Surname,
			},
			cancellationToken);

	/// <summary>
	/// Удалить пользователя
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	[HttpDelete]
	[Authorize]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task DeleteAsync(CancellationToken cancellationToken)
		=> await _mediator.Send(new DeleteUserCommand(), cancellationToken);
}
