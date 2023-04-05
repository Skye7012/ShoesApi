﻿using MediatR;
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
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<SignUpUserResponse>> SignUp(SignUpUserCommand request)
			=> CreatedAtAction(nameof(Get), await _mediator.Send(request));

		/// <summary>
		/// Авторизоваться
		/// </summary>
		/// <param name="request">Запрос</param>
		/// <returns>Authorization token</returns>
		[HttpPost("SignIn")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<SignInUserResponse> SignIn(SignInUserCommand request) 
			=> await _mediator.Send(request);

		/// <summary>
		/// Обновить данные о пользователе
		/// </summary>
		/// <param name="request">Запрос</param>
		[HttpPut]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task Put(PutUserCommand request)
			=> await _mediator.Send(request);

		/// <summary>
		/// Удалить пользователя
		/// </summary>
		[HttpDelete]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task Delete()
			=> await _mediator.Send(new DeleteUserCommand());
	}
}
