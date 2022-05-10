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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BrandConfiguration());

			//modelBuilder.Entity<Brand>(entity =>
			//{
			//	entity.ToTable("brands");

			//	entity.HasComment("Брэнды");

			//	entity.HasIndex(e => e.Name, "Brand_name_key")
			//		.IsUnique();

			//	entity.Property(e => e.Id)
			//		.HasColumnName("id")
			//		.HasDefaultValueSql("nextval('\"Brand_id_seq\"'::regclass)");

			//	entity.Property(e => e.Name).HasColumnName("name");
			//});

			modelBuilder.Entity<Destination>(entity =>
			{
				entity.ToTable("destination");

				entity.HasComment("Назначение обуви");

				entity.HasIndex(e => e.Name, "destination_name_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name).HasColumnName("name");
			});

			modelBuilder.Entity<Season>(entity =>
			{
				entity.ToTable("season");

				entity.HasComment("Сезон");

				entity.HasIndex(e => e.Name, "season_name_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name).HasColumnName("name");
			});

			modelBuilder.Entity<Shoe>(entity =>
			{
				entity.ToTable("shoes");

				entity.HasComment("Обувь");

				entity.HasIndex(e => e.Image, "shoes_image_key")
					.IsUnique();

				entity.HasIndex(e => e.Name, "shoes_name_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.BrandId).HasColumnName("brand_id");

				entity.Property(e => e.DestinationId).HasColumnName("destination_id");

				entity.Property(e => e.Image)
					.HasColumnName("image")
					.HasComment("Название изображения (для пути)");

				entity.Property(e => e.Name).HasColumnName("name");

				entity.Property(e => e.Price)
					.HasColumnName("price")
					.HasComment("Цена");

				entity.Property(e => e.SeasonId).HasColumnName("season_id");

				entity.HasOne(d => d.Brand)
					.WithMany(p => p.Shoes)
					.HasForeignKey(d => d.BrandId)
					.HasConstraintName("shoes_brand_id_fkey");

				entity.HasOne(d => d.Destination)
					.WithMany(p => p.Shoes)
					.HasForeignKey(d => d.DestinationId)
					.HasConstraintName("shoes_destination_id_fkey");

				entity.HasOne(d => d.Season)
					.WithMany(p => p.Shoes)
					.HasForeignKey(d => d.SeasonId)
					.HasConstraintName("shoes_season_id_fkey");
			});
		}
	}
}
