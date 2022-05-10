using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Брэнды
	/// </summary>
	public partial class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.HasComment("Брэнды");
			builder.HasKey(e => e.Id);

			builder.Property(e => e.Id)
				.HasDefaultValueSql("nextval('\"Brand_id_seq\"'::regclass)");
				//.HasColumnType("serial");

			builder.Property(e => e.Name);

			builder.HasIndex(e => e.Name)
				.IsUnique();
		}
	}
}
