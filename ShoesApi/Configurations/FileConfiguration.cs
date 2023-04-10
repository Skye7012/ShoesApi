using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="File"/>
	/// </summary>
	public class FileConfiguration : EntityBaseConfiguration<File>
	{
		/// <inheritdoc/>
		public override void ConfigureChild(EntityTypeBuilder<File> builder)
		{
			builder.HasComment("Файлы");

			builder.Property(e => e.Name)
				.IsRequired();

			builder
				.HasMany(t => t.Shoes)
				.WithOne(pt => pt.ImageFile)
				.HasForeignKey(pt => pt.ImageFileId)
				.HasPrincipalKey(t => t.Id)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
