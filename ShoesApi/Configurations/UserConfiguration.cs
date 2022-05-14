using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesApi.Entities
{
	/// <summary>
	/// Конфигурация для <see cref="User"/>
	/// </summary>
	public class UserConfiguration : EntityBaseConfiguration<User>
	{
		public override void ConfigureChild(EntityTypeBuilder<User> builder)
		{
			builder.HasComment("Пользователи");

			builder.Property(e => e.Login);
			builder.Property(e => e.PasswordHash);
			builder.Property(e => e.PasswordSalt);

			builder.HasIndex(e => e.Login)
				.IsUnique();
		}
	}
}
