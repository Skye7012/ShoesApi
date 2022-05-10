﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="Season"/>
	/// </summary>
	public class SeasonConfiguration : EntityBaseConfiguration<Season>
	{
		public override void ConfigureChild(EntityTypeBuilder<Season> builder)
		{
			builder.HasComment("Сезон");

			builder.Property(e => e.Name);

			builder.HasIndex(e => e.Name)
				.IsUnique();

			builder.HasMany(d => d.Shoes)
				.WithOne(p => p.Season)
				.HasForeignKey(d => d.SeasonId)
				.HasPrincipalKey(p => p.Id);
		}
	}
}