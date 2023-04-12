using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.Domain.Entities;
using ShoesApi.Infrastructure.Persistence.Common;

namespace ShoesApi.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для <see cref="Shoe"/>
/// </summary>
public class ShoeConfiguration : EntityBaseConfiguration<Shoe>
{
	/// <inheritdoc/>
	public override void ConfigureChild(EntityTypeBuilder<Shoe> builder)
	{
		builder.HasComment("Обувь");

		builder.Property(e => e.Name);
		builder.Property(e => e.ImageFileId);
		builder.Property(e => e.Price)
			.HasComment("Цена");

		builder.HasIndex(e => e.Name)
			.IsUnique();

		builder.HasOne(d => d.ImageFile)
			.WithMany(p => p.Shoes)
			.HasForeignKey(d => d.ImageFileId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder.HasOne(d => d.Brand)
			.WithMany(p => p.Shoes)
			.HasForeignKey(d => d.BrandId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder.HasOne(d => d.Destination)
			.WithMany(p => p.Shoes)
			.HasForeignKey(d => d.DestinationId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder.HasOne(d => d.Season)
			.WithMany(p => p.Shoes)
			.HasForeignKey(d => d.SeasonId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder.HasMany(d => d.Sizes)
			.WithMany(p => p.Shoes)
			.UsingEntity(e => e.ToTable("shoes_sizes"));

		builder
			.HasMany(t => t.OrderItems)
			.WithOne(pt => pt.Shoe)
			.HasForeignKey(pt => pt.ShoeId)
			.HasPrincipalKey(t => t.Id)
			.OnDelete(DeleteBehavior.ClientCascade);
	}
}
