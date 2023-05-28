using System.Text;
using HostInitActions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio.AspNetCore;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Infrastructure.InitExecutors;
using ShoesApi.Infrastructure.Persistence;
using ShoesApi.Infrastructure.Services;

namespace ShoesApi.Infrastructure;

/// <summary>
/// Конфигуратор сервисов
/// </summary>
public static class InfrastructureServicesConfigurator
{
	/// <summary>
	/// Сконфигурировать сервисы
	/// </summary>
	/// <param name="builder">Билдер приложения</param>
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configurationManager)
		=> services
			.AddAuthorization(configurationManager)
			.AddDatabase(configurationManager)
			.AddS3Storage(configurationManager)
			.AddInitExecutors();

	/// <summary>
	/// Сконфигурировать сервисы авторизации
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configurationManager">Менеджер конфигурации приложения</param>
	private static IServiceCollection AddAuthorization(this IServiceCollection services, ConfigurationManager configurationManager)
	{
		services
			.AddScoped<IUserService, UserService>()
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
						.GetBytes(configurationManager.GetSection("AppSettings:Token").Value!)),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
				});
		return services;
	}

	/// <summary>
	/// Сконфигурировать подключение к БД
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configurationManager">Менеджер конфигурации приложения</param>
	private static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
	{
		var connString = configurationManager.GetConnectionString("ShoesDb")!;
		services.AddDbContext<ApplicationDbContext>(opt =>
			{
				opt.UseNpgsql(connString);
				opt.UseSnakeCaseNamingConvention();
			});

		return services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
	}

	/// <summary>
	/// Сконфигурировать хранилище S3
	/// </summary>
	/// <param name="services">Сервисы</param>
	/// <param name="configurationManager">Менеджер конфигурации приложения</param>
	private static IServiceCollection AddS3Storage(this IServiceCollection services, ConfigurationManager configurationManager)
	{
		var connString = new Uri(configurationManager.GetConnectionString("S3")!);
		services.AddMinio(connString);

		return services.AddTransient<IS3Service, S3Service>();
	}

	/// <summary>
	/// Сконфигурировать инициализаторы сервисов
	/// </summary>
	/// <param name="services">Сервисы</param>
	private static IServiceCollection AddInitExecutors(this IServiceCollection services)
	{
		services.AddAsyncServiceInitialization()
			.AddInitActionExecutor<DbInitExecutor>()
			.AddInitActionExecutor<S3InitExecutor>();

		return services;
	}
}
