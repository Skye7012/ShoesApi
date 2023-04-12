using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.Application.Common.Exceptions;

namespace ShoesApi.Api.Configurators;

/// <summary>
/// Конфигуратор middleware обработчика ошибок
/// </summary>
public static class ExceptionMiddlewareConfigurator
{
	/// <summary>
	/// Сконфигурировать middleware для обработчика ошибок
	/// </summary>
	/// <param name="app">Билдер веб приложения</param>
	public static void UseExceptionHandlerMiddleware(this WebApplication app)
	{
		var logger = app.Services.GetService<ILogger<Program>>()!;

		app.UseExceptionHandler(c => c.Run(async context =>
		{
			var exception = context.Features
				.Get<IExceptionHandlerPathFeature>()!
				.Error;

			logger.LogError(exception.Message);

			if (exception is ApplicationExceptionBase applicationException)
				context.Response.StatusCode = (int)applicationException.StatusCode;

			var response = new ProblemDetails() { Detail = exception.Message };

			await context.Response.WriteAsJsonAsync(response);
		}));
	}
}
