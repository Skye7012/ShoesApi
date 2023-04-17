using Microsoft.Net.Http.Headers;

namespace ShoesApi.Api.Configurators;

/// <summary>
/// Конфигуратор веб приложения
/// </summary>
public static class WebAppConfigurator
{
	/// <summary>
	/// Сконфигурировать сервисы
	/// </summary>
	/// <param name="app">Билдер веб приложения</param>
	public static void ConfigureWebApp(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseHttpLogging();
		}

		// Использование middleware обработчика ошибок
		app.UseExceptionHandlerMiddleware();

		ConfigureCors(app);

		app.UseHttpsRedirection()
			.UseAuthentication()
			.UseAuthorization();

		app.MapControllers();
	}

	/// <summary>
	/// Сконфигурировать CORS
	/// </summary>
	/// <param name="app">Билдер веб приложения</param>
	private static void ConfigureCors(WebApplication app)
	{
		var allowedOrigin = app.Configuration.GetSection("AppSettings:AllowedOrigin").Value
			?? throw new Exception("Не указан AllowedOrigin");

		app.UseCors(
		options =>
			options
				.WithOrigins(allowedOrigin)
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithExposedHeaders(HeaderNames.ContentDisposition));
	}
}
