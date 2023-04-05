using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShoesApi.IntegrationTests
{
	/// <summary>
	/// Расширения для <see cref="IServiceCollection"/>
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Удалить контекст БД из DI
		/// </summary>
		/// <typeparam name="T">Тип контекста БД</typeparam>
		/// <param name="services">Коллекция сервисов</param>
		public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
		{
			var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
			if (descriptor != null) services.Remove(descriptor);
		}

		/// <summary>
		/// Удостовериться, что БД было создано
		/// </summary>
		/// <typeparam name="T">Тип контекста БД</typeparam>
		/// <param name="services">Коллекция сервисов</param>
		public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
		{
			var serviceProvider = services.BuildServiceProvider();

			using var scope = serviceProvider.CreateScope();
			var scopedServices = scope.ServiceProvider;
			var context = scopedServices.GetRequiredService<T>();
			context.Database.EnsureCreated();
		}
	}
}
