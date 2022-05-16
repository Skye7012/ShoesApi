using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="Order"/>
	/// </summary>
	public class OrderConfiguration : EntityBaseConfiguration<Order>
	{
		public override void ConfigureChild(EntityTypeBuilder<Order> builder)
		{
			builder.HasComment("Заказ");

			builder.Property(e => e.OrderDate);
			builder.Property(e => e.Sum);
			builder.Property(e => e.Count);

			builder.HasMany(d => d.Shoes)
				.WithMany(p => p.Orders)
				.UsingEntity<OrderShoe>(
					j => j
						.HasOne(pt => pt.Shoe)
						.WithMany(t => t.OrderShoes)
						.HasForeignKey(pt => pt.ShoeId)
						.HasPrincipalKey(t => t.Id),
					j => j
						.HasOne(pt => pt.Order)
						.WithMany(t => t.OrderShoes)
						.HasForeignKey(pt => pt.OrderId)
						.HasPrincipalKey(t => t.Id),
					//j => j
					//	.HasOne(pt => pt.Size)
					//	.WithMany(t => t.OrderShoes)
					//	.HasForeignKey(pt => pt.SizeId)
					//	.HasPrincipalKey(t => t.Id),
					j =>
					{
						j.ToTable("orders_shoes");
						j.HasKey(t => new { t.OrderId, t.ShoeId, t.SizeId });
					});
		}
	}
}
