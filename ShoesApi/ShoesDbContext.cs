using Microsoft.EntityFrameworkCore;
using ShoesApi.Entities;

namespace ShoesApi
{
	public class ShoesDbContext : DbContext
	{
		public ShoesDbContext()
		{
		}

		public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Brand> Brands { get; set; } = null!;
		public virtual DbSet<Destination> Destinations { get; set; } = null!;
		public virtual DbSet<Season> Seasons { get; set; } = null!;
		public virtual DbSet<Shoe> Shoes { get; set; } = null!;
		public virtual DbSet<User> Users { get; set; } = null!;
		public virtual DbSet<Size> Sizes { get; set; } = null!;
		public virtual DbSet<Order> Orders { get; set; } = null!;
		public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;

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
