using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="Shoe"/>
	/// </summary>
	public class ShoeConfiguration : EntityBaseConfiguration<Shoe>
	{
		public override void ConfigureChild(EntityTypeBuilder<Shoe> builder)
		{
			builder.HasComment("Обувь");

			builder.Property(e => e.Name);
			builder.Property(e => e.Image)
				.HasComment("Название изображения (для пути)");
			builder.Property(e => e.Price)
				.HasComment("Цена");

			builder.HasIndex(e => e.Name)
				.IsUnique();
			builder.HasIndex(e => e.Image)
				.IsUnique();

			builder.HasOne(d => d.Brand)
				.WithMany(p => p.Shoes)
				.HasForeignKey(d => d.BrandId)
				.HasPrincipalKey(p => p.Id);

			builder.HasOne(d => d.Destination)
				.WithMany(p => p.Shoes)
				.HasForeignKey(d => d.DestinationId)
				.HasPrincipalKey(p => p.Id);

			builder.HasOne(d => d.Season)
				.WithMany(p => p.Shoes)
				.HasForeignKey(d => d.SeasonId)
				.HasPrincipalKey(p => p.Id);

			builder.HasMany(d => d.Sizes)
				.WithMany(p => p.Shoes)
				.UsingEntity(e => e.ToTable("shoes_sizes"));
		}
	}
}
