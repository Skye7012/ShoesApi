using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Infrastructure.Persistence;
using Xunit;

namespace ShoesApi.IntegrationTests;

/// <summary>
/// Базовый класс для интеграционных тестов
/// </summary>
[Collection("FactoryCollection")]
public class IntegrationTestsBase : IAsyncLifetime
{
	protected readonly IntegrationTestFactory<Program, ApplicationDbContext> _factory;

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="factory">Фабрика приложения</param>
	protected IntegrationTestsBase(IntegrationTestFactory<Program, ApplicationDbContext> factory)
	{
		_factory = factory;
		Client = factory.CreateClient();
		DbContext = factory.Services.CreateScope()
			.ServiceProvider.GetRequiredService<IApplicationDbContext>();
		S3Service = factory.Services.GetRequiredService<IS3Service>();
		UserService = factory.Services.CreateScope()
			.ServiceProvider.GetRequiredService<IUserService>();
		DateTimeProvider = factory.Services.GetRequiredService<IDateTimeProvider>();

		Seeder = new IntegrationTestSeeder(DbContext, UserService);
	}

	/// <summary>
	/// Http клиент
	/// </summary>
	protected HttpClient Client { get; }

	/// <summary>
	/// Контекст БД
	/// </summary>
	protected IApplicationDbContext DbContext { get; }

	/// <summary>
	/// Хранилище S3
	/// </summary>
	protected IS3Service S3Service { get; }

	/// <summary>
	/// Сервис пользователя
	/// </summary>
	protected IUserService UserService { get; }

	/// <summary>
	/// Сидер
	/// </summary>
	protected IntegrationTestSeeder Seeder { get; }

	/// <summary>
	/// Respawner БД
	/// </summary>
	protected Respawner Respawner { get; private set; } = default!;

	/// <summary>
	/// Провайдер времени
	/// </summary>
	public IDateTimeProvider DateTimeProvider { get; private set; }

	/// <summary>
	/// Тестовый поток для <see cref="S3Service"/>
	/// </summary>
	protected MemoryStream S3ServiceTestStream { get; } = new MemoryStream(new byte[] { 1, 2 });

	/// <inheritdoc/>
	public async Task DisposeAsync()
	{
		await Respawner.ResetAsync(_factory.DbConnection);

		await Seeder.ReseedInitialDataAsync();
	}

	/// <inheritdoc/>
	public async Task InitializeAsync()
	{
		await Seeder.SeedInitialDataAsync();

		Respawner = await Respawner.CreateAsync(
			_factory.DbConnection,
			new RespawnerOptions
			{
				DbAdapter = DbAdapter.Postgres,
				WithReseed = true,
			});
	}

	/// <summary>
	/// Аутентифицироваться
	/// </summary>
	/// <param name="token">Токен аутентификации</param>
	protected void Authenticate(string? token = null)
		=> Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			"bearer",
			token ?? UserService.CreateToken(Seeder.AdminUser));
}
