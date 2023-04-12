using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Users.Commands.DeleteUser;
using ShoesApi.Application.Users.Commands.PutUser;
using ShoesApi.Application.Users.Commands.SignInUser;
using ShoesApi.Application.Users.Commands.SignUpUser;
using ShoesApi.Application.Users.Queries.GetUser;

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
		SignUpUserCommand request,
		CancellationToken cancellationToken)
			=> CreatedAtAction(
				nameof(GetAsync),
				await _mediator.Send(request, cancellationToken));

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
		SignInUserCommand request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(request, cancellationToken);

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
		PutUserCommand request,
		CancellationToken cancellationToken)
		=> await _mediator.Send(request, cancellationToken);

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
