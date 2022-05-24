﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Базовая конфигурация
	/// </summary>
	public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
		where TEntity : EntityBase
	{
		public void Configure(EntityTypeBuilder<TEntity> builder)
		{
			ConfigureBase(builder);
			ConfigureChild(builder);
		}

		private static void ConfigureBase(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id);
		}

		public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
	}
}
