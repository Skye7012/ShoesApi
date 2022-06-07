using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Configuration for entities that inherit <see cref="EntityBase"/>
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
		/// Configure <see cref="EntityBase"/>
		/// </summary>
		/// <param name="builder">The builder to be used to configure the entity type</param>
		private static void ConfigureBase(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id);
		}

		/// <summary>
		/// Configure <typeparamref name="TEntity" /> child of <see cref="EntityBase"/>
		/// </summary>
		/// <param name="builder">The builder to be used to configure the entity type</param>
		public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
	}
}
