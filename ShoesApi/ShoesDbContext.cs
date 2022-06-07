using Microsoft.EntityFrameworkCore;
using ShoesApi.Entities;

namespace ShoesApi
{
	/// <inheritdoc/>
	public class ShoesDbContext : DbContext
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ShoesDbContext()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="options">Options</param>
		public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Brands
		/// </summary>
		public virtual DbSet<Brand> Brands { get; set; } = null!;

		/// <summary>
		/// Destinations
		/// </summary>
		public virtual DbSet<Destination> Destinations { get; set; } = null!;

		/// <summary>
		/// Seasons
		/// </summary>
		public virtual DbSet<Season> Seasons { get; set; } = null!;

		/// <summary>
		/// Shoes
		/// </summary>
		public virtual DbSet<Shoe> Shoes { get; set; } = null!;

		/// <summary>
		/// Users
		/// </summary>
		public virtual DbSet<User> Users { get; set; } = null!;

		/// <summary>
		/// Sizes
		/// </summary>
		public virtual DbSet<Size> Sizes { get; set; } = null!;

		/// <summary>
		/// Orders
		/// </summary>
		public virtual DbSet<Order> Orders { get; set; } = null!;

		/// <summary>
		/// OrderItems
		/// </summary>
		public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;


		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BrandConfiguration());
			modelBuilder.ApplyConfiguration(new DestinationConfiguration());
			modelBuilder.ApplyConfiguration(new SeasonConfiguration());
			modelBuilder.ApplyConfiguration(new ShoeConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new SizeConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
		}
	}
}
