using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.Domain.Common;

namespace ShoesApi.Infrastructure.Persistence.Common;

/// <summary>
/// Конфигурация для сущностей, которые наследуются от <see cref="EntityBase"/>
/// </summary>
public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
	where TEntity : EntityBase
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<TEntity> builder)
	{
		ConfigureBase(builder);
		ConfigureChild(builder);
	}

	/// <summary>
	/// Сконфигурировать базовые поля из <see cref="EntityBase"/>
	/// </summary>
	/// <param name="builder">Билдер</param>
	private static void ConfigureBase(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Id);
	}

	/// <summary>
	/// Сконфигурировать <typeparamref name="TEntity" /> которая является дочерней от <see cref="EntityBase"/>
	/// </summary>
	/// <param name="builder">Билдер</param>
	public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
}
