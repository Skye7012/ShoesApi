using Microsoft.EntityFrameworkCore;
using ShoesApi.Application.Common.Interfaces;
using ShoesApi.Domain.Entities;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using File = ShoesApi.Domain.Entities.File;

namespace ShoesApi.Infrastructure.Persistence;

/// <inheritdoc/>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	/// <summary>
	/// Конструктор
	/// </summary>
	public ApplicationDbContext()
	{
	}

	/// <summary>
	/// Конструктор
	/// </summary>
	/// <param name="options">Опции контекста</param>
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	/// <inheritdoc/>
	public DbContext Instance => this;

	/// <summary>
	/// Брэнды обуви
	/// </summary>
	public DbSet<Brand> Brands { get; private set; } = default!;

	/// <summary>
	/// Назначения обуви
	/// </summary>
	public DbSet<Destination> Destinations { get; private set; } = default!;

	/// <summary>
	/// Сезоны обуви
	/// </summary>
	public DbSet<Season> Seasons { get; private set; } = default!;

	/// <summary>
	/// Кроссовки
	/// </summary>
	public DbSet<Shoe> Shoes { get; private set; } = default!;

	/// <summary>
	/// Пользователи
	/// </summary>
	public DbSet<User> Users { get; private set; } = default!;

	/// <summary>
	/// Размеры обуви
	/// </summary>
	public DbSet<Size> Sizes { get; private set; } = default!;

	/// <summary>
	/// Заказы
	/// </summary>
	public DbSet<Order> Orders { get; private set; } = default!;

	/// <summary>
	/// Части заказов
	/// </summary>
	public DbSet<OrderItem> OrderItems { get; private set; } = default!;

	/// <summary>
	/// Части заказов
	/// </summary>
	public DbSet<File> Files { get; private set; } = default!;

	/// <inheritdoc/>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
		=> modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
