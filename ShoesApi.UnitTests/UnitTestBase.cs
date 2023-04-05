using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NSubstitute;
using ShoesApi.Services;
using ShoesApi.UnitTests.Mocks;

namespace ShoesApi.UnitTests
{
	/// <summary>
	/// Базовый класс для unit тестов
	/// </summary>
	public class UnitTestBase
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public UnitTestBase()
		{
			UserService = new TestUserService();
			DateTimeProvider = Substitute.For<IDateTimeProvider>();

			ConfigureDateTimeProvider();
		}

		/// <summary>
		/// Сервис пользователя для тестов
		/// </summary>
		public TestUserService UserService { get; private set; }

		/// <summary>
		/// Провайдер времени
		/// </summary>
		public IDateTimeProvider DateTimeProvider { get; private set; }

		/// <summary>
		/// Создать контекст БД
		/// </summary>
		/// <returns>Контекст БД</returns>
		public ShoesDbContext CreateInMemoryContext(Action<ShoesDbContext>? seedActions = null)
		{
			var context = new ShoesDbContext(new DbContextOptionsBuilder<ShoesDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
				.Options);

			// Инициализация начальных данных
			context.Users.Add(UserService.AdminUser);

			seedActions?.Invoke(context);

			context.SaveChanges();
			context.ChangeTracker.Clear();
			return context;
		}

		/// <summary>
		/// Сконфигурировать <see cref="DateTimeProvider"/>
		/// </summary>
		private void ConfigureDateTimeProvider()
		{
			DateTimeProvider.UtcNow
				.Returns(DateTime.SpecifyKind(
					new DateTime(2020, 01, 01),
					DateTimeKind.Utc));
		}
	}
}
