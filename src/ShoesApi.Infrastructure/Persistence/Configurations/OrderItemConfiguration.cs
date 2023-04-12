using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.Domain.Entities;
using ShoesApi.Infrastructure.Persistence.Common;

namespace ShoesApi.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для <see cref="OrderItem"/>
/// </summary>
public class OrderItemConfiguration : EntityBaseConfiguration<OrderItem>
{
	/// <inheritdoc/>
	public override void ConfigureChild(EntityTypeBuilder<OrderItem> builder)
	{
		builder.HasComment("Часть заказа");

		builder.HasIndex(x => new { x.OrderId, x.ShoeId, x.SizeId })
			.IsUnique();

		builder.HasOne(d => d.Order)
			.WithMany(p => p.OrderItems)
			.HasForeignKey(d => d.OrderId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder
			.HasOne(pt => pt.Shoe)
			.WithMany(t => t.OrderItems)
			.HasForeignKey(pt => pt.ShoeId)
			.HasPrincipalKey(t => t.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder
			.HasOne(pt => pt.Size)
			.WithMany(t => t.OrderItems)
			.HasForeignKey(pt => pt.SizeId)
			.HasPrincipalKey(t => t.Id)
			.OnDelete(DeleteBehavior.ClientCascade);
	}
}
