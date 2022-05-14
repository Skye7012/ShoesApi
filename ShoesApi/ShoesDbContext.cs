using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BrandConfiguration());
			modelBuilder.ApplyConfiguration(new DestinationConfiguration());
			modelBuilder.ApplyConfiguration(new SeasonConfiguration());
			modelBuilder.ApplyConfiguration(new ShoeConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
		}
	}
}
