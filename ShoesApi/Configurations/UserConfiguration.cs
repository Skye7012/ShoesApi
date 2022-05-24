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
			builder.Property(e => e.Name);
			builder.Property(e => e.Fname);
			builder.Property(e => e.Phone);

			builder.HasIndex(e => e.Login)
				.IsUnique();

			builder.HasMany(d => d.Orders)
				.WithOne(p => p.User)
				.HasForeignKey(d => d.UserId)
				.HasPrincipalKey(p => p.Id)
				.OnDelete(DeleteBehavior.ClientCascade);
		}
	}
}
