using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Configuration for <see cref="Destination"/>
	/// </summary>
	public class DestinationConfiguration : EntityBaseConfiguration<Destination>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<Destination> builder)
		{
			builder.HasComment("Назначение обуви");

			builder.Property(e => e.Name);

			builder.HasIndex(e => e.Name)
				.IsUnique();

			builder.HasMany(d => d.Shoes)
				.WithOne(p => p.Destination)
				.HasForeignKey(d => d.DestinationId)
				.HasPrincipalKey(p => p.Id)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
