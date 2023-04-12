using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.Domain.Entities;
using ShoesApi.Infrastructure.Persistence.Common;

namespace ShoesApi.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для <see cref="Order"/>
/// </summary>
public class OrderConfiguration : EntityBaseConfiguration<Order>
{
	/// <inheritdoc/>
	public override void ConfigureChild(EntityTypeBuilder<Order> builder)
	{
		builder.HasComment("Заказ");

		builder.Property(e => e.OrderDate);
		builder.Property(e => e.Sum);
		builder.Property(e => e.Count);
		builder.Property(e => e.Address);

		builder.HasOne(d => d.User)
			.WithMany(p => p.Orders)
			.HasForeignKey(d => d.UserId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);

		builder.HasMany(d => d.OrderItems)
			.WithOne(p => p.Order)
			.HasForeignKey(d => d.OrderId)
			.HasPrincipalKey(p => p.Id)
			.OnDelete(DeleteBehavior.ClientCascade);
	}
}
