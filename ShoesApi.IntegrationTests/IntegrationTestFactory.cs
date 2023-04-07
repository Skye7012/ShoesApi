using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using NSubstitute;
using ShoesApi.Services;
using Testcontainers.PostgreSql;
using Xunit;

namespace ShoesApi.IntegrationTests
{
	/// <summary>
	/// Фабрика приложения для интеграционных тестов
	/// </summary>
	/// <typeparam name="TProgram">Точка входа</typeparam>
	/// <typeparam name="TDbContext">Контекст БД</typeparam>
	public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
		where TProgram : class where TDbContext : DbContext
	{
		private readonly PostgreSqlContainer _container;

		/// <summary>
		/// Конструктор
		/// </summary>
		public IntegrationTestFactory()
		{
			_container = new PostgreSqlBuilder()
				.WithDatabase("test_db")
				.WithUsername("postgres")
				.WithPassword("postgres")
				.WithImage("postgres:14")
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
				services.AddDbContext<TDbContext>(options => { options.UseNpgsql(_container.GetConnectionString()); });
				services.EnsureDbCreated<TDbContext>();

				services.RemoveAll<IDateTimeProvider>();
				services.AddSingleton<IDateTimeProvider>(GetDateTimeProviderMock());
			});
		}

		/// <inheritdoc/>
		public async Task InitializeAsync()
		{
			await _container.StartAsync();
			DbConnection = new NpgsqlConnection(_container.GetConnectionString());
			await DbConnection.OpenAsync();
		}

		/// <inheritdoc/>
		public new async Task DisposeAsync() => await _container.DisposeAsync();

		/// <summary>
		/// Получить мок <see cref="IDateTimeProvider"/>
		/// </summary>
		/// <returns>Мок <see cref="IDateTimeProvider"/></returns>
		private IDateTimeProvider GetDateTimeProviderMock()
		{
			var dateTimeProvider = Substitute.For<IDateTimeProvider>();

			dateTimeProvider.UtcNow
				.Returns(DateTime.SpecifyKind(
					new DateTime(2020, 01, 01),
					DateTimeKind.Utc));

			return dateTimeProvider;
		}
	}
}
