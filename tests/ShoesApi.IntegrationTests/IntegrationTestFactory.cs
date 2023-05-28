using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Minio;
using Minio.AspNetCore;
using Npgsql;
using NSubstitute;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.IntegrationTests.Extensions;
using Testcontainers.Minio;
using Testcontainers.PostgreSql;
using Xunit;

namespace ShoesApi.IntegrationTests;

/// <summary>
/// Фабрика приложения для интеграционных тестов
/// </summary>
/// <typeparam name="TProgram">Точка входа</typeparam>
/// <typeparam name="TDbContext">Контекст БД</typeparam>
public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
	where TProgram : class where TDbContext : DbContext
{
	private readonly PostgreSqlContainer _dbContainer;
	private readonly MinioContainer _s3Container;

	/// <summary>
	/// Конструктор
	/// </summary>
	public IntegrationTestFactory()
	{
		_dbContainer = new PostgreSqlBuilder()
			.WithName("test_db")
			.WithDatabase("test_db")
			.WithUsername("postgres")
			.WithPassword("postgres")
			.WithImage("postgres:14")
			.WithCleanUp(true)
			.Build();

		_s3Container = new MinioBuilder()
			.WithName("test_minio")
			.WithUsername("MinioUser")
			.WithPassword("MinioPassword")
			.WithCleanUp(true)
			.Build();
	}

	/// <summary>
	/// Подключение к БД
	/// </summary>
	public DbConnection DbConnection { get; private set; } = default!;

	/// <inheritdoc/>
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureTestServices(services =>
		{
			services.RemoveDbContext<TDbContext>();
			services.AddDbContext<TDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));

			services.RemoveAll<MinioClient>();
			services.AddMinio(new Uri($"s3://MinioUser:MinioPassword@localhost:{_s3Container.GetMappedPublicPort(9000)}"));

			services.RemoveAll<IDateTimeProvider>();
			services.AddSingleton(GetDateTimeProviderMock());
		});

		builder.UseEnvironment(Environments.Staging);
	}

	/// <inheritdoc/>
	public async Task InitializeAsync()
	{
		await _dbContainer.StartAsync();
		DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
		await DbConnection.OpenAsync();

		await _s3Container.StartAsync();
	}

	/// <inheritdoc/>
	public new async Task DisposeAsync()
	{
		await _dbContainer.DisposeAsync();
		await _s3Container.DisposeAsync();
	}

	/// <summary>
	/// Получить мок <see cref="IDateTimeProvider"/>
	/// </summary>
	/// <returns>Мок <see cref="IDateTimeProvider"/></returns>
	private static IDateTimeProvider GetDateTimeProviderMock()
	{
		var dateTimeProvider = Substitute.For<IDateTimeProvider>();

		dateTimeProvider.UtcNow
			.Returns(DateTime.SpecifyKind(
				new DateTime(2020, 01, 01),
				DateTimeKind.Utc));

		return dateTimeProvider;
	}
}
