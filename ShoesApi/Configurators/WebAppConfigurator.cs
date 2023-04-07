using Microsoft.Net.Http.Headers;

namespace ShoesApi.Configurators
{
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
			}

			// Использование middleware обработчика ошибок
			app.UseExceptionHandlerMiddleware();

			app.UseCors(
				options =>
					options
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.WithExposedHeaders(HeaderNames.ContentDisposition));

			app.UseHttpsRedirection()
				.UseAuthentication()
				.UseAuthorization();

			app.MapControllers();
		}
	}
}
