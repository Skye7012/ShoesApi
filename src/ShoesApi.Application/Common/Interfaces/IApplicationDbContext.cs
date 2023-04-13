using Microsoft.EntityFrameworkCore;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Application.Common.Interfaces;

/// <summary>
/// Интерфейс контекста БД данного приложения
/// </summary>
public interface IApplicationDbContext : IDbContext
{
	/// <summary>
	/// Брэнды обуви
	/// </summary>
	DbSet<Brand> Brands { get; }

	/// <summary>
	/// Назначения обуви
	/// </summary>
	DbSet<Destination> Destinations { get; }

	/// <summary>
	/// Сезоны обуви
	/// </summary>
	DbSet<Season> Seasons { get; }

	/// <summary>
	/// Кроссовки
	/// </summary>
	DbSet<Shoe> Shoes { get; }

	/// <summary>
	/// Пользователи
	/// </summary>
	DbSet<User> Users { get; }

	/// <summary>
	/// Размеры обуви
	/// </summary>
	DbSet<Size> Sizes { get; }

	/// <summary>
	/// Заказы
	/// </summary>
	DbSet<Order> Orders { get; }

	/// <summary>
	/// Части заказов
	/// </summary>
	DbSet<OrderItem> OrderItems { get; }

	/// <summary>
	/// Части заказов
	/// </summary>
	DbSet<File> Files { get; }

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <param name="cancellationToken">Токен отмены</param>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Сохранить изменения
	/// </summary>
	/// <returns>Количество записей состояния, записанных в базу данных</returns>
	int SaveChanges();
}
