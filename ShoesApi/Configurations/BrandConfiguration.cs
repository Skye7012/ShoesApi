﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="Brand"/>
	/// </summary>
	public class BrandConfiguration : EntityBaseConfiguration<Brand>
	{
		public override void ConfigureChild(EntityTypeBuilder<Brand> builder)
		{
			builder.HasComment("Брэнды");

			builder.Property(e => e.Name);

			builder.HasIndex(e => e.Name)
				.IsUnique();

			builder.HasMany(d => d.Shoes)
				.WithOne(p => p.Brand)
				.HasForeignKey(d => d.BrandId)
				.HasPrincipalKey(p => p.Id)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
