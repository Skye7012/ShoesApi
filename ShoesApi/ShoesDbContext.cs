using Microsoft.EntityFrameworkCore;
using ShoesApi.Entities;
using ShoesApi.Entities.ShoeSimpleFilters;

namespace ShoesApi
{
	/// <inheritdoc/>
	public class ShoesDbContext : DbContext
	{
		/// <summary>
		/// Конструктор
		/// </summary>
		public ShoesDbContext()
		{
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="options">Опции контекста</param>
		public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Брэнды обуви
		/// </summary>
		public virtual DbSet<Brand> Brands { get; set; } = default!;

		/// <summary>
		/// Назначения обуви
		/// </summary>
		public virtual DbSet<Destination> Destinations { get; set; } = default!;

		/// <summary>
		/// Сезоны обуви
		/// </summary>
		public virtual DbSet<Season> Seasons { get; set; } = default!;

		/// <summary>
		/// Кроссовки
		/// </summary>
		public virtual DbSet<Shoe> Shoes { get; set; } = default!;

		/// <summary>
		/// Пользователи
		/// </summary>
		public virtual DbSet<User> Users { get; set; } = default!;

		/// <summary>
		/// Размеры обуви
		/// </summary>
		public virtual DbSet<Size> Sizes { get; set; } = default!;

		/// <summary>
		/// Заказы
		/// </summary>
		public virtual DbSet<Order> Orders { get; set; } = default!;

		/// <summary>
		/// Части заказов
		/// </summary>
		public virtual DbSet<OrderItem> OrderItems { get; set; } = default!;


		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
			=> modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoesDbContext).Assembly);
	}
}
