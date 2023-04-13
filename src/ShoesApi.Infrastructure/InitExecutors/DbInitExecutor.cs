using HostInitActions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoesApi.Infrastructure.Persistence;

namespace ShoesApi.Infrastructure.InitExecutors;

/// <summary>
/// Инициализатор базы данных
/// </summary>
public class DbInitExecutor : IAsyncInitActionExecutor
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly IWebHostEnvironment _environment;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="scopeFactory">Фабрика скоупов</param>
	/// <param name="environment">Переменные среды</param>
	public DbInitExecutor(
		IServiceScopeFactory scopeFactory,
		IWebHostEnvironment environment)
	{
		_scopeFactory = scopeFactory;
		_environment = environment;
	}

	/// <summary>
	/// Провести инициализацию
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		using var scope = _scopeFactory.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		if (_environment.IsStaging())
		{
			await db.Database.EnsureCreatedAsync(cancellationToken);
			return;
		}

		await db.Database.MigrateAsync(cancellationToken);
	}
}
