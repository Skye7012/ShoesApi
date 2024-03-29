using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesApi.Domain.Entities.ShoeSimpleFilters;
using ShoesApi.Infrastructure.Persistence.Common;
using ShoesApi.Infrastructure.Persistence.InitialData;

namespace ShoesApi.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для <see cref="Brand"/>
/// </summary>
public class BrandConfiguration : EntityBaseConfiguration<Brand>
{
	/// <inheritdoc/>
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

		builder.HasData(InitialDataStorage.Brands);
	}
}
