using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NSubstitute;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Infrastructure.Persistence;
using ShoesApi.UnitTests.Mocks;

namespace ShoesApi.UnitTests;

/// <summary>
/// Базовый класс для unit тестов
/// </summary>
public class UnitTestBase
{
	/// <summary>
	/// Конструктор
	/// </summary>
	protected UnitTestBase()
	{
		UserService = new TestUserService();
		DateTimeProvider = Substitute.For<IDateTimeProvider>();
		S3Service = Substitute.For<IS3Service>();

		ConfigureDateTimeProvider();
		ConfigureS3Service();
	}

	/// <summary>
	/// Сервис пользователя для тестов
	/// </summary>
	protected TestUserService UserService { get; }

	/// <summary>
	/// Провайдер времени
	/// </summary>
	protected IDateTimeProvider DateTimeProvider { get; }

	/// <summary>
	/// Хранилище S3
	/// </summary>
	protected IS3Service S3Service { get; }

	/// <summary>
	/// Тестовый поток для <see cref="S3Service"/>
	/// </summary>
	protected Stream S3ServiceTestStream { get; } = new MemoryStream(new byte[] { 1, 2 });

	/// <summary>
	/// Создать контекст БД
	/// </summary>
	/// <returns>Контекст БД</returns>
	protected IApplicationDbContext CreateInMemoryContext(Action<IApplicationDbContext>? seedActions = null)
	{
		var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
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

	/// <summary>
	/// Сконфигурировать <see cref="S3Service"/>
	/// </summary>
	private void ConfigureS3Service()
	{
		S3Service.DownloadAsync(Arg.Any<string>(), default)
			.Returns(Task.FromResult(S3ServiceTestStream));
	}
}
