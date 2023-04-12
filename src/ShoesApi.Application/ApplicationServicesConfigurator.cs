using Microsoft.Extensions.DependencyInjection;

namespace ShoesApi.Application;

/// <summary>
/// Конфигуратор сервисов
/// </summary>
public static class ApplicationServicesConfigurator
{
	/// <summary>
	/// Сконфигурировать сервисы слоя Application
	/// </summary>
	/// <param name="services">Сервисы</param>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		=> services
			.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServicesConfigurator).Assembly));
}
