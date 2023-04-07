using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.InitialData;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="Size"/>
	/// </summary>
	public class SizeConfiguration : EntityBaseConfiguration<Size>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<Size> builder)
		{
			builder.HasComment("Размеры");

			builder.Property(e => e.RuSize);

			builder.HasIndex(e => e.RuSize)
				.IsUnique();

			builder.HasMany(d => d.Shoes)
				.WithMany(p => p.Sizes)
				.UsingEntity(e => e.ToTable("shoes_sizes"));

			builder
				.HasMany(t => t.OrderItems)
				.WithOne(pt => pt.Size)
				.HasForeignKey(pt => pt.SizeId)
				.HasPrincipalKey(t => t.Id)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.HasData(InitialDataStorage.Sizes);
		}
	}
}
